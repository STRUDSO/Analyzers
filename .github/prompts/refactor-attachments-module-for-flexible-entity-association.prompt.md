---
mode: agent
description: "Refactor Attachments Module for flexible entity association and POST API endpoint."
---

# Refactor Attachments Module for Flexible Entity Association

## Context

- The Attachments Module is currently hardcoded to only allow uploading attachments to Meetings.
- The goal is to decouple Attachments from Meetings, enabling future support for uploading attachments to other entities (e.g., MeetingGroup).
- No migration or backward compatibility is required.
- The API endpoint for uploading attachments should use HTTP POST.

## Requirements

- Decouple Attachments from Meetings; support associating attachments with any target entity (Meeting, MeetingGroup, etc.).
- Use a generic association model: `AttachmentTargetType` (enum/string) and `TargetId` (Guid).
- Update domain, application, infrastructure, and API layers to support flexible attachment targets.
- Ensure security, validation, and authorization patterns are preserved.
- Update tests to cover new scenarios for all supported target types.
- Document new patterns and update module README.
- API endpoint for uploading attachments must use HTTP POST.

## Implementation Steps

1. **Analysis & Discovery**
   - Review current Attachments domain model, repository, command/handler, and API endpoint.
   - Identify all usages of MeetingId in the Attachments module.

2. **Domain Model Refactor**
   - Refactor Attachment aggregate to include `AttachmentTargetType` and `TargetId`.
   - Update factory methods and constructors to require target type and id.
   - Update domain events to include target type/id.

3. **Application Layer Update**
   - Refactor commands (e.g., `UploadAttachmentCommand`) to accept target type/id.
   - Update handlers to validate target existence based on type (Meeting, MeetingGroup, etc.).
   - Refactor validators and authorizers to support multiple target types.

4. **Infrastructure Layer Update**
   - Update repository interfaces and implementations to query by target type/id.
   - Ensure Marten mappings and serialization support new fields.

5. **API Layer Refactor**
   - Define the route: `POST /attachments/{targetType}/{targetId}/upload`
   - Accept file and metadata in the request body (e.g., multipart/form-data).
   - Remove Meeting-specific endpoints.
   - Update OpenAPI documentation and ProblemDetails responses.

6. **Testing**
   - Refactor and extend unit tests for domain, application, and infrastructure layers.
   - Add new tests for MeetingGroup attachment scenarios.
   - Update integration tests to cover new API endpoints and flows.
   - Ensure security tests (authorization, enumeration attack prevention) are updated.

7. **Documentation**
   - Update module README to describe new patterns and usage.
   - Add ADR if architectural decisions change.

8. **Review & Validation**
   - Run full test suite (`dotnet test`).
   - Format code (`dotnet format`).
   - Review for DDD, security, and modularity compliance.

## References

- [copilot-instructions.md](../copilot-instructions.md)
- [COOKBOOK.md](../../docs/development/COOKBOOK.md)
- [consolidated_learnings.md](../../docs/consolidated_learnings.md)
- [BuildingBlocks.md](../../docs/BuildingBlocks.md)
- [DevelopmentOfAFeature.md](../../docs/DevelopmentOfAFeature.md)

## Variables

- `${workspaceFolder}`: Workspace root directory
- `${file}`: Current file path
- `${input:targetType}`: Target entity type (e.g., Meeting, MeetingGroup)
- `${input:targetId}`: Target entity ID
