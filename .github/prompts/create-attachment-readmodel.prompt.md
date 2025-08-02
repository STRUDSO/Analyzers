---
mode: agent
description: "Create a read model in the Attachment module to store meeting and organizer information."
---

# Task: Create a Read Model in the Attachment Module

## Overview
This task involves creating a read model in the Attachment module to store information about published meetings and their organizers. The read model will:

1. Listen to integration events published by the Meeting Groups module.
2. Store meeting and organizer information.
3. Be used to verify:
   - If a meeting exists.
   - If a user is authorized to upload attachments to a meeting.

## Requirements

### Integration Events
- **Consume**:
  - `MeetingPublishedIntegrationEvent`: Add meeting details to the read model.
  - `MeetingUnpublishedIntegrationEvent`: Remove meeting details from the read model.
  - `MeetingOrganizerChangedIntegrationEvent`: Update the organizer in the read model.

### Read Model
- Fields:
  - `MeetingId` (GUID)
  - `OrganizerId` (GUID)

### Repository
- Define an interface `IMeetingReadModelRepository`.
- Implement the repository:
  - Use an in-memory dictionary for testing.
  - Use a persistent storage mechanism (e.g., Marten) for production.

### Event Handlers
- Implement handlers for the above integration events to update the read model.

### Testing
- Unit tests for event handlers.
- Integration tests to verify the read model's behavior.

## Implementation Steps

### 1. Define the Read Model
- Create a new class `MeetingReadModel` in the `MjoMeet.Attachments.Application` namespace.
- Fields:
  - `MeetingId` (GUID)
  - `OrganizerId` (GUID)

### 2. Create the Repository
- Define an interface `IMeetingReadModelRepository` in `MjoMeet.Attachments.Application`.
- Implement the repository in `MjoMeet.Attachments.Infrastructure`:
  - Use an in-memory dictionary for testing.
  - Use a persistent storage mechanism (e.g., Marten) for production.

### 3. Implement Event Handlers
- Add event handlers in `MjoMeet.Attachments.Application`:
  - `MeetingPublishedIntegrationEventHandler`:
    - Add a new entry to the read model with `MeetingId` and `OrganizerId`.
  - `MeetingUnpublishedIntegrationEventHandler`:
    - Remove the entry from the read model.
  - `MeetingOrganizerChangedIntegrationEventHandler`:
    - Update the `OrganizerId` in the read model.

### 4. Update the Attachment Module
- Add a reference to `MjoMeet.MeetingGroups.Integration` to consume the integration events.
- Register the event handlers in the DI container.

### 5. Testing
- **Unit Tests**:
  - Test each event handler to ensure the read model is updated correctly.
- **Integration Tests**:
  - Simulate the flow of integration events and verify the read model's state.

## Testing Plan

### Unit Tests
1. **MeetingPublishedIntegrationEventHandler**:
   - Verify that a new meeting is added to the read model.
2. **MeetingUnpublishedIntegrationEventHandler**:
   - Verify that the meeting is removed from the read model.
3. **MeetingOrganizerChangedIntegrationEventHandler**:
   - Verify that the organizer is updated in the read model.

### Integration Tests
1. Simulate publishing a meeting and verify that it is added to the read model.
2. Simulate unpublishing a meeting and verify that it is removed from the read model.
3. Simulate changing the organizer and verify that the organizer is updated in the read model.

## Notes
- The read model will not store the meeting status as it is not required for the current use case.
- Focus on simplicity and efficiency in the implementation.
