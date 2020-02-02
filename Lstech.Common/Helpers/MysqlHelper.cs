using Dapper;
using Lstech.Common.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Common.Helpers
{
    /// <summary>
    /// Mysql 数据库操作类
    /// </summary>
    public class MysqlHelper
    {
        public static string MysqlConn { get; set; }

        public static string GetConn
        {
            get
            {
                return MysqlConn;
            }
        }

        public static IDbConnection OpenMysqlConnection(string connStr)
        {
            if (!string.IsNullOrWhiteSpace(connStr))
            {
                var conn = new MySqlConnection(connStr);
                conn.Open();
                return conn;
            }
            throw new ArgumentException("传入的连接字符串为空!");
        }

        #region Synchronization

        #region 增删改

        public static int ExecuteSql(IDbConnection conn, string sql, object param)
        {
            try
            {
                int i = conn.Execute(sql, param);
                return i;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public static int ExecuteSql(IDbConnection conn, string sql, object param, IDbTransaction transaction = null)
        {
            try
            {
                int i = conn.Execute(sql, param, transaction);
                return i;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="order"></param>
        /// <param name="sqlstr"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IEnumerable<T> Page<T>(IDbConnection conn, string order, string sqlstr, int pageIndex, int pageSize, out int count, dynamic param = null)
        {
            count = QueryCount(conn, sqlstr, param);
            //string sql = string.Format(@"select * from( select top {0} row_number() over (order by {1}) row, w1.* from ({2})w1 )w2 where  w2.row > {3} ", pageIndex * pageSize, order, sqlstr, (pageIndex - 1) * pageSize);
            string sql = string.Format(@"select * from ({0}) A order by {1} limit {2},{3}", sqlstr, order, (pageIndex - 1) * pageSize, pageSize);
            var cr = SqlMapper.Query<T>(conn, sql, param);
            return cr;
        }

        /// <summary>
        /// 返回数量
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sqlstr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int QueryCount(IDbConnection conn, string sqlstr, dynamic param = null)
        {
            string sql = string.Format("SELECT count(1) num FROM  ({0}) A", sqlstr);
            var cr = SqlMapper.Query(conn, sql, param);
            var singleOrDefault = ((IEnumerable<dynamic>)cr).SingleOrDefault();
            if (singleOrDefault != null)
            {
                if (singleOrDefault.num.GetType() != typeof(int))
                    return (int)singleOrDefault.num;
                else
                    return singleOrDefault.num;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 执行SQL，返回第一行第一个元素的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ExecuteSql_First<T>(IDbConnection conn, string sql, object param)
        {
            try
            {
                T result = conn.Query<T>(sql, param).FirstOrDefault<T>();
                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 执行SQL，返回数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<T> ExecuteSql_ToList<T>(IDbConnection conn, string sql, object param)
        {
            try
            {
                IEnumerable<T> result = conn.Query<T>(sql, param);
                return result.ToList<T>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 执行存存储过程
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="SpName">名称</param>
        /// <param name="parems">参数据对像</param>
        /// <returns></returns>
        public static int ExecStoredProcedure(IDbConnection conn, string SpName, object parems)
        {
            int res = 0;
            try
            {
                res = Dapper.SqlMapper.Execute(conn, SpName, parems, null, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return res;
        }

        #endregion

        #region Asynchronous

        #region 增删改

        public static async Task<int> ExecuteSqlAsync(IDbConnection conn, string sql, object param)
        {
            try
            {
                return await conn.ExecuteAsync(sql, param);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static async Task<int> ExecuteSqlAsync(IDbConnection conn, string sql, object param, IDbTransaction transaction = null)
        {
            try
            {
                return await conn.ExecuteAsync(sql, param, transaction);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="order"></param>
        /// <param name="sqlstr"></param>
        /// <param name="page"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> QueryPageAsync<T>(IDbConnection conn, string order, string sqlstr, PageModel page, dynamic param = null)
        {
            page.TotalCount = await QueryCountAsync(conn, sqlstr, param);
            string sql = string.Format(@"select * from ({0}) A order by {1} limit {2},{3}", sqlstr, order, (page.PageIndex - 1) * page.PageSize, page.PageSize);
            return await SqlMapper.QueryAsync<T>(conn, sql, param);
        }

        /// <summary>
        /// 返回数量
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sqlstr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<int> QueryCountAsync(IDbConnection conn, string sqlstr, dynamic param = null)
        {
            string sql = string.Format("SELECT count(1) num FROM  ({0}) A", sqlstr);
            var cr = await SqlMapper.QueryAsync(conn, sql, param);
            var singleOrDefault = ((IEnumerable<dynamic>)cr).SingleOrDefault();
            if (singleOrDefault != null)
            {
                if (singleOrDefault.num.GetType() != typeof(int))
                    return (int)singleOrDefault.num;
                else
                    return singleOrDefault.num;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 执行SQL，返回第一行第一个元素的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<T> QueryFirstAsync<T>(IDbConnection conn, string sql, object param)
        {
            try
            {
                return await conn.QueryFirstAsync<T>(sql, param);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 执行SQL，返回第一行第一个元素的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection conn, string sql, object param)
        {
            try
            {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 执行SQL，返回数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> QueryListAsync<T>(IDbConnection conn, string sql, object param = null)
        {
            try
            {
                return await SqlMapper.QueryAsync<T>(conn, sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 执行SQL，返回数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> QueryListAsync<T>(IDbConnection conn, string sql, string order, object param = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(order))
                    sql = string.Format(@"select * from ({0}) A order by {1} ", sql, order);
                return await SqlMapper.QueryAsync<T>(conn, sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 执行存存储过程
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="SpName">名称</param>
        /// <param name="parems">参数据对像</param>
        /// <returns></returns>
        public static async Task<int> ExecStoredProcedureAsync(IDbConnection conn, string SpName, object parems)
        {
            int res = 0;
            try
            {
                res = await Dapper.SqlMapper.ExecuteAsync(conn, SpName, parems, null, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return res;
        }

        #endregion
    }
}
