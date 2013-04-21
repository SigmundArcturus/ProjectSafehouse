INSERT INTO 
	[dbo].[Statuses]
	([ID]
	,[CompanyId]
	,[Name]
	,[Description])
VALUES
    (
		NEWID(),
		null,
		'Open',
		'An open action item that is still being worked on'
	)

INSERT INTO 
	[dbo].[Statuses]
	([ID]
	,[CompanyId]
	,[Name]
	,[Description])
VALUES
    (
		NEWID(),
		null,
		'Closed',
		'A closed action item that is no longer being worked on'
	)

INSERT INTO 
	[dbo].[Statuses]
	([ID]
	,[CompanyId]
	,[Name]
	,[Description])
VALUES
    (
		NEWID(),
		null,
		'Pending Approval',
		'An action item that is waiting to be approved by management'
	)
