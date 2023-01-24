from lib2to3.pgen2.driver import Driver
import pandas as pd
import pypyodbc as odbc
#tre sa ii dau un pipinstall

#server name: NICOLAE\LOCALDB#63E3A4D8

DRIVER_NAME = 'SQL SERVER'
SERVER_NAME = 'NICOLAE\LOCALDB#40A84A14'
DATABASE_NAME = 'Clienti'

connection_string = f"""
    DRIVER = {{{DRIVER_NAME}}};
    SERVER = {SERVER_NAME};
    DATABASE = {DATABASE_NAME}; 
    Trusted_Connection=yes;
"""

conn = odbc.connect(connection_string)
print(conn)
