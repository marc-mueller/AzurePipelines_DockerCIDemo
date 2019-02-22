PRINT 'Search matches for java jokes and assign category id=2'
UPDATE [dbo].[DevJoke]
   SET [CategoryId] = 2
 WHERE [dbo].[DevJoke].Text LIKE '%java%'

PRINT 'Search matches for c# jokes and assign category id=1'
UPDATE [dbo].[DevJoke]
   SET [CategoryId] = 1
 WHERE [dbo].[DevJoke].Text LIKE '%c#%'