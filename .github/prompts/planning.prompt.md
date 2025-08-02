---
mode: 'agent'
description: 'Generate a new implementation plan for a feature'
---

You are a senior software engineer with extensive experience in implementing new features in complex systems. You have been tasked with creating a comprehensive plan for the implementation of a new feature in the MjoMeet system. The plan must be detailed and include all necessary steps, considerations, and unit tests.

YOU MUST use the Sequential Thinking MCP when you planning.
YOU MUST use context7 to get the latest documentation for the external libraries and frameworks you will use. 

You should start by analyzing the requirements of the feature, breaking it down into manageable components, and then outlining the implementation steps in a clear and structured manner.

You are now going to create a comprehensive plan for the implementation of this new feature I told you about, so please take your time and think deeply about how you will implement it.

You are allowed to provide pseudo code in you plan.

YOU MUST include all the unit test that you will need so I can review them. They should contain the implementation you are going to use.

Write your plan into docs/features/{module}/{Use-case}/{Use-case}-plan.md

DO NOT start implementing of the use-case before im happy with the plan.

# Implementation Checklist

## Refined Development Workflow

Based on this implementation experience, here's the recommended workflow for future features:

### 1. **Pre-Implementation Analysis** (NEW)

- [ ] Security threat modeling and mitigation planning
- [ ] Domain event storming with security considerations
- [ ] Privacy and compliance requirement analysis
- [ ] Performance and scalability planning

### 2. **Domain-First Development**

- [ ] Value objects with robust validation
- [ ] Aggregates with encapsulated business rules
- [ ] Domain events for state change communication
- [ ] Repository interfaces with security considerations

### 3. **Security-Aware Application Layer**

- [ ] Commands with factory methods and validation
- [ ] Handlers with security-first design
- [ ] Domain event listeners with proper error handling
- [ ] Scheduled commands with serialization support

### 4. **Infrastructure & Monitoring**

- [ ] Privacy-preserving telemetry implementation
- [ ] Rate limiting and abuse prevention
- [ ] Global exception handling with security considerations
- [ ] API endpoints with proper documentation

### 5. **Comprehensive Testing**

- [ ] Security-focused test cases
- [ ] Domain logic unit tests
- [ ] Integration tests for end-to-end workflows
- [ ] Performance and load testing

### 6. **Security Review & Documentation**

- [ ] Penetration testing and security assessment
- [ ] Documentation of security measures and considerations
- [ ] Compliance verification and audit trail review

## For future features, use this checklist based on lessons learned:

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
