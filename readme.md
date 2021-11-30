# Starter project for .net core

Started from .netcore api project in Visual Studio .net

What it provides:
1. All needed packages present
1. Add swagger
1. Add automapper
1. Logs to file
   1. **WebApi/Logs**
1. Simple injections - no AutoFac - because overhead, setup hell and test hell (at least for me)
   1. **Startup.cs/ConfigureServices**
   1. Almost each project has a **IServiceCollectionExtension.cs** in it's root
1. Tests
   1. I explicitly wanted them in separate projects, as they have separate enviroments 
   1. **Units Tests**
      1. Use mocks, make as fast as possible, test as much as possible
   3. **Integration Tests**
      1. ~~Use new db every time a test/test suit is run~~
      1. Use same test db(over time it will become more significant), and just erase redundant data
         1. by hand
         2. using *InitializeDbForTests*
   3. After running a new migration on the db, the easiest way to upgrade test db is to *//uncomment this after a new migration has been run on dev* (found in **CustomWebApplicationFactory.cs**)
   1. 
1. Simple separation Api / Services / Data
   1. it will become more complicated later :)
1. Further splitting of Data:
   1. **Data.Domain** it's very important it's not dependant of anything!
      1. Entities (code first)
      2. Dto's
      3. Mapper
   1. **Data.Migrations**
      1. Important to be separate as we do not want to pollute the **Data.Domain**
      1. instructions to run migration are in *readme.txt*
      3. I further suffixed it with the database type, hence **Data.Migrations.Postgres**
   1. **Data.Repository**
      4. Exactly because we use injections AND migrations, I do not want to be dependant of a specifc implementation. Enter repository pattern :)
1. **Utilities** - self explanatory
   1. just crypt md5 for starter
   1. Basic concept: it has ZERO links to current soluttion. It can be copy/paste in any other soluttion
   1. That's why it's the single project that has the luxury of using static functions
      1. Except *IServiceCollectionExtension*, but that's an extension method and is required by inject architecture