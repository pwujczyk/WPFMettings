CREATE TABLE [Meeting] (
  [MeetingId] int IDENTITY (1,1) NOT NULL
, [Date] datetime NOT NULL
, [BeforeNotes] ntext NULL
, [DuringNotes] ntext NULL
, [AfterNotes] ntext NULL
);
GO
ALTER TABLE [Meeting] ADD CONSTRAINT [PK_Meeting] PRIMARY KEY ([MeetingId]);
GO
