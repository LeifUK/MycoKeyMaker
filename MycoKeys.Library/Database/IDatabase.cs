﻿namespace MycoKeys.Library.Database
{
    public interface IDatabase
    {
        string Name { get; }
        SqlQueryBuilders.ISqlQueryBuilder ISqlQueryBuilder { get; set; }
        void CloseConnection();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
