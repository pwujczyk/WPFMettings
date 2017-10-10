
ALTER TABLE [MeetingsContacts] ADD CONSTRAINT [FK_MeetingContact_Contact] FOREIGN KEY ([ContactId]) REFERENCES Contact(ContactId)
GO
