---
mode: agent
description: "Refactor Attachments Module to use Marten's built-in soft delete functionality."
---

# Refactor Attachments Module to Use Marten Soft Delete

This prompt guides you through refactoring the Attachments Module to leverage Marten's built-in soft delete, replacing any custom or manual deletion logic. It incorporates all relevant context, requirements, and best practices from previous discussions and project documentation.

## Context
- The Attachments Module currently does not use Marten's built-in soft delete.
- Marten provides native support for soft deleting documents, which simplifies code and ensures consistent query behavior.
- Project conventions and DDD patterns must be followed (see [docs/BuildingBlocks.md](../../docs/BuildingBlocks.md), [docs/consolidated_learnings.md](../../docs/consolidated_learnings.md), and [copilot-instructions.md](../copilot-instructions.md)).

## Requirements
- All aggregate/document types in the Attachments Module (e.g., `Attachment`) must use Marten's soft delete.
- Repository delete operations must use Marten's soft delete API (e.g., `DeleteSoftlyAsync`).
- Remove any custom `IsDeleted` flags or manual soft delete logic.
- Queries must, by default, exclude soft-deleted documents (Marten default behavior).
- Add or update tests to verify soft delete behavior.
- Update documentation and module README as needed.
- Ensure backward compatibility and data migration if custom soft delete flags were previously used.

## Implementation Steps
1. **Discovery**
   - Identify all aggregates/entities in the Attachments Module.
   - Locate all repository interfaces and implementations for attachments.
   - Search for custom soft delete logic (e.g., `IsDeleted` flags, manual filtering).
2. **Enable Marten Soft Delete**
   - Update Marten document mappings for attachments to enable soft delete (e.g., `.SoftDeleted()` in Marten configuration).
   - Refactor repository `Delete` methods to use Marten's soft delete API.
   - Remove custom `IsDeleted` or similar flags from aggregates/entities.
3. **Refactor Queries**
   - Ensure all queries exclude soft-deleted documents by default.
   - For queries that need to include soft-deleted documents, use Marten's API to include them explicitly.
4. **Update Tests**
   - Refactor or add tests to verify:
     - Soft-deleted attachments are not returned in normal queries.
     - Soft-deleted attachments can be restored (if required).
     - Deletion operations do not physically remove documents.
   - Remove or update tests that check for custom soft delete flags.
5. **Documentation**
   - Update module README and any relevant documentation to describe the new soft delete behavior.
   - Document how to query for soft-deleted documents if needed.
6. **Migration**
   - If existing data uses a custom soft delete flag, consider a migration script to mark those as soft-deleted in Marten.
   - Ensure no breaking changes for API consumers.
7. **Code Formatting & Review**
   - Run `dotnet format` to ensure code style consistency.
   - Review all changes for adherence to project conventions.

## Testing
- Unit tests for repository delete and query methods (ensure soft delete is respected).
- Integration tests for API endpoints that delete or query attachments.
- Tests for restoring soft-deleted attachments (if supported).
- Regression tests to ensure no accidental data loss or exposure of deleted data.

## References
- [docs/BuildingBlocks.md](../../docs/BuildingBlocks.md)
- [docs/consolidated_learnings.md](../../docs/consolidated_learnings.md)
- [copilot-instructions.md](../copilot-instructions.md)
- [Marten Soft Delete Documentation](https://martendb.io/documents/soft-deletes.html)

---

**Rationale:**
Using Marten's built-in soft delete simplifies the codebase, leverages proven infrastructure, and aligns with best practices for document storage in .NET applications.

---

_This prompt is reusable for similar refactoring tasks involving Marten soft delete in other modules._
