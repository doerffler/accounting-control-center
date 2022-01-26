using System;

namespace DoePaAdminDataAdapter
{

    [AttributeUsage(AttributeTargets.Property)]
    class DBColumnAttribute : Attribute
    {

        public string ColumnName { get; set; }

        public DBColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }

    }
}
