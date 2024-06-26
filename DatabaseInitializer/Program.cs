using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";

            string databaseName = "CurrencyExchange";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create database
                string createDbQuery = $"IF DB_ID('{databaseName}') IS NULL CREATE DATABASE {databaseName};";
                using (SqlCommand createDbCommand = new SqlCommand(createDbQuery, connection))
                {
                    createDbCommand.ExecuteNonQuery();
                }

                // Use database
                string useDbQuery = $"USE {databaseName};";
                using (SqlCommand useDbCommand = new SqlCommand(useDbQuery, connection))
                {
                    useDbCommand.ExecuteNonQuery();
                }

                // Create tables
                string createTablesQuery = @"
                    CREATE TABLE Currency (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Country NVARCHAR(50),
                        Name NVARCHAR(50),
                        Abbreviation NVARCHAR(10)
                    );

                    CREATE TABLE CurrencyPair (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        BaseCurrencyId INT,
                        QuoteCurrencyId INT,
                        MinValue DECIMAL(18,4),
                        MaxValue DECIMAL(18,4),
                        FOREIGN KEY (BaseCurrencyId) REFERENCES Currency(Id),
                        FOREIGN KEY (QuoteCurrencyId) REFERENCES Currency(Id)
                    );

                    INSERT INTO Currency (Country, Name, Abbreviation) VALUES
                    ('United States', 'Dollar', 'USD'),
                    ('Eurozone', 'Euro', 'EUR'),
                    ('Japan', 'Yen', 'JPY'),
                    ('United Kingdom', 'Pound', 'GBP');

                    INSERT INTO CurrencyPair (BaseCurrencyId, QuoteCurrencyId, MinValue, MaxValue) VALUES
                    ((SELECT Id FROM Currency WHERE Abbreviation='USD'), (SELECT Id FROM Currency WHERE Abbreviation='EUR'), 0.8000, 1.2000),
                    ((SELECT Id FROM Currency WHERE Abbreviation='USD'), (SELECT Id FROM Currency WHERE Abbreviation='JPY'), 100.00, 150.00),
                    ((SELECT Id FROM Currency WHERE Abbreviation='GBP'), (SELECT Id FROM Currency WHERE Abbreviation='USD'), 1.2000, 1.8000);
                ";
                using (SqlCommand createTablesCommand = new SqlCommand(createTablesQuery, connection))
                {
                    createTablesCommand.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Database and tables created successfully!");
        }
    }
}
