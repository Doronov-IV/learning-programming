USE MessengerDatabase
GO



--/*
SELECT MSG.AuthorId
FROM Messages AS MSG
WHERE MSG.Contents = 'hello'
--*/



--/*
SELECT USR.Login
FROM Users AS USR
WHERE USR.Id = (SELECT MSG.AuthorId
				FROM Messages AS MSG
				WHERE MSG.Contents = 'hello')
--*/



--/*
SELECT USR.Login, USR.PublicId
FROM Users AS USR
WHERE USR.Id = (SELECT MSG.AuthorId
				FROM Messages AS MSG
				WHERE MSG.Contents = (SELECT MSG.Contents
									  FROM Messages AS MSG
									  WHERE MSG.Id = 1935))
--*/