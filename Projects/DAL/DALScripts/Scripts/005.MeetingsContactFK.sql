
ALTER TABLE [MeetingsContacts] ADD CONSTRAINT [FK_MeetingContact_Meeting] FOREIGN KEY ([MeetingId]) REFERENCES Meeting(MeetingId)
GO
