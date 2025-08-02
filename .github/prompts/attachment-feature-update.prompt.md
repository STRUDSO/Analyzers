---
mode: agent
description: "Implement updates to the Attachment feature, including repository, aggregate, and command handler changes."
---

# Task Overview

This prompt guides the implementation of updates to the Attachment feature in the MjoMeet project. The tasks include:

1. Implementing a real `MartenRepository` for `IMeetingReadModelRepository`.
2. Refactoring the `Attachment` aggregate to use a `Guid` value object for `Uploader`.
3. Modifying `DeleteAttachmentCommandHandler` to only mark attachments as deleted.
4. Adding a domain event listener to schedule file deletion.
5. Creating a new command handler for actual file deletion.

# Implementation Steps

## 1. Implement `MartenRepository` for `IMeetingReadModelRepository`
- Create a new class `MartenMeetingReadModelRepository` in the `Infrastructure` layer.
- Implement all methods defined in `IMeetingReadModelRepository` using Marten.
- Register the repository in the dependency injection container.

## 2. Refactor `Attachment` Aggregate
- Replace `UploaderUserId` with a value object `Uploader` of type `Guid`.
- Update the `Create` and `RequestDelete` methods to use the new value object.
- Refactor all validations and domain events to use `Guid` instead of `string`.

## 3. Modify `DeleteAttachmentCommandHandler`
- Remove the logic for deleting files from the handler.
- Ensure it only calls `RequestDelete` on the `Attachment` aggregate.
- Update the unit tests to reflect the new behavior.

## 4. Add Domain Event Listener
- Create a new class `AttachmentDeletedEventListener`.
- Listen for `AttachmentDeletedDomainEvent`.
- Schedule a new command `DeleteAttachmentFileCommand` for actual file deletion.

## 5. Create New Command Handler
- Create a new command `DeleteAttachmentFileCommand`.
- Implement a handler `DeleteAttachmentFileCommandHandler` to delete files from storage.
- Ensure it logs errors and retries if necessary.

# Testing Plan

## Unit Tests
- Test all methods in `MartenMeetingReadModelRepository`.
- Validate the `Uploader` value object in the `Attachment` aggregate.
- Ensure `DeleteAttachmentCommandHandler` only marks attachments as deleted.
- Verify the domain event listener schedules the new command.
- Test the new command handler for file deletion.

## Integration Tests
- Test the end-to-end flow of deleting an attachment, including marking it as deleted and removing the file.
- Validate the interaction between the domain event listener and the new command handler.

## Edge Cases
- Ensure the system handles concurrent deletion requests gracefully.
- Test scenarios where the file is already deleted or inaccessible.

# References

- Relevant files:
  - `IMeetingReadModelRepository` in `src/Modules/Attachments/MjoMeet.Attachments.Domain/IMeetingReadModelRepository.cs`
  - `Attachment` aggregate in `src/Modules/Attachments/MjoMeet.Attachments.Domain/Attachments/Attachment.cs`
  - `DeleteAttachmentCommandHandler` in `src/Modules/Attachments/MjoMeet.Attachments.Application/DeleteAttachment/DeleteAttachmentCommand.cs`
