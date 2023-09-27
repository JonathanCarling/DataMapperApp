namespace DataMapperApp.Helpers
{
    public static class SqlCommandHelper
    {
        public enum CommandType
        {
            SELECT,
            SELECT_ALL,
            INSERT,
            UPDATE,
            DELETE
        }

        public static class CommandText
        {
            public static string ENTITY_RETRIEVE     = "SELECT * FROM Entity WHERE Id = {0}";
            public static string ENTITY_RETRIEVE_ALL = "SELECT * FROM Entity";
            public static string ENTITY_CREATE       = "INSERT INTO Entity (Name, Created, Updated) VALUES ('{0}', '{1}', '{2}')";
            public static string ENTITY_UPDATE       = "UPDATE Entity SET Name = '{1}', Updated = '{2}' where Id = {0}";
            public static string ENTITY_DELETE       = "DELETE FROM Entity WHERE Id = {0}";
        }

        public static string BuildCommand(CommandType commandType, params string[] args)
        {
            string command = string.Empty;

            switch (commandType)
            {
                case CommandType.SELECT:
                    command = string.Format(CommandText.ENTITY_RETRIEVE, args); ;
                    break;
                case CommandType.SELECT_ALL:
                    command = CommandText.ENTITY_RETRIEVE_ALL;
                    break;
                case CommandType.INSERT:
                    command = string.Format(CommandText.ENTITY_CREATE, args);
                    break;
                case CommandType.UPDATE:
                    command = string.Format(CommandText.ENTITY_UPDATE, args);
                    break;
                case CommandType.DELETE:
                    command = string.Format(CommandText.ENTITY_DELETE, args);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return command;
        }
    }
}