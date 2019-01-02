### Migration guide
1) Use Package Manager Console to add new migration or run other migration operations.

2) New migration:
````powershell
add-migration <MigrationName> -Context Kpd37Gomel.DataAccess.ApplicationDbContext -Project Kpd37Gomel.DataAccess -StartupProject Kpd37Gomel
````

3) Update database with the specific migration:
````powershell
update-database -Migration <MigrationName> -Context Kpd37Gomel.DataAccess.ApplicationDbContext -Project Kpd37Gomel.DataAccess -StartupProject Kpd37Gomel
````

4) Update databse with all existing migrations. This method will ignore migrations that already applied to database.
````powershell
update-database -Context Kpd37Gomel.DataAccess.ApplicationDbContext -Project Kpd37Gomel.DataAccess -StartupProject Kpd37Gomel
````

5) Migration tool use Connection String from appsettings.json file placed in -StartupProject.




https://scotch.io/tutorials/using-sass-with-the-angular-cli

https://scotch.io/tutorials/angular-shortcut-to-importing-styles-files-in-components