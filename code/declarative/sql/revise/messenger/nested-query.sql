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



--/*
SELECT USR.Login, MSG.Contents
FROM Users AS USR, Messages AS MSG
WHERE LEN(MSG.Contents) = 5 AND MSG.AuthorId = USR.Id
--*/