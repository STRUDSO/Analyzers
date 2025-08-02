---
mode: 'agent'
---

You must use Sequential Thinking when your planning.

# Implementation Checklist

For the feature I described, here's the recommended workflow based on lessons learned:

1. **Define the Feature**: Clearly outline the feature requirements and acceptance criteria.
2. **Design the Solution**: Create a high-level design, including architecture diagrams and data flow.
3. **Implement the Feature**: Write the code, following best practices and coding standards.
4. **Test the Feature**: Create unit tests, integration tests, and end-to-end tests to validate the implementation.
5. **Review the Code**: Conduct code reviews to ensure quality and adherence to standards.

For future features, use this checklist based on lessons learned:

### Security & Privacy

- [ ] Threat model completed
- [ ] Enumeration attack prevention implemented
- [ ] Rate limiting configured
- [ ] PII protection in telemetry
- [ ] Consistent error responses
- [ ] Authentication/authorization implemented

### Architecture & Design

- [ ] Domain events for state changes
- [ ] Value objects with validation
- [ ] Repository pattern with security considerations
- [ ] Scheduled commands with serialization support
- [ ] Global exception handling

### Testing & Quality

- [ ] Security test cases
- [ ] Domain logic unit tests
- [ ] Integration tests for workflows
- [ ] Performance testing
- [ ] Code duplication eliminated

### Monitoring & Operations

- [ ] Privacy-preserving telemetry
- [ ] Performance monitoring
- [ ] Security event monitoring
- [ ] Error tracking and alerting
- [ ] Audit trail (if required)

This checklist ensures consistent quality and security across all feature implementations in the MjoMeet system.