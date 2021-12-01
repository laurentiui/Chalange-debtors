1. assumptions
   1. if a debtor exists and it's closed
      1. it can not be updated
      2. a new debtor with the same number can not be inserted (this is taken care by default by the key property)
      3. buk update is done outside working hours. If there is an urgency for a specific debtor he can be updated using CRUD
2. no optimization is done. recommended for a real live scenario (assuming millions of debtors and file with > 10k records to update 
   1. closed debtors in different table
   2. list is passed to a cron job (service), that compiles it outside working hours, than sends a report
   3. functions NEEDED in DB
      1. fnGetInvalidIds(xmlList) - list of debtors in file but that are already closed
      2. fnGetDebtorsToAddIds(xmlList)
      3. fnGetDebtorsToUpdate(xmlList)
      4. fnGetDebtorsToIgnore(xmlList) - they should be updated but the info is the same
   4. based on the prevoius lists, an update is done and report send
3. For object compare a hash should be used. I am using prop by prop compare just for clarity of code
4. Again, for optimizations reason, I did not cover the fact that between checking "header lists" (who should be updated/inserted etc) and the actual db update changes might occur
5. changes
   1. I changed "Number" to "Id" just to fit existing code
6. Should be nice
   7. Add insert/update/close bulk
     1. easy to do just left it for the end if I have time :)