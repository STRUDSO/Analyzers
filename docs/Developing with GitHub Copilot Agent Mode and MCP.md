# Developing with GitHub Copilot Agent Mode and MCP

This guide summarizes the workflow and best practices for using GitHub Copilot Agent Mode with Model Context Protocol (MCP), based on Austen Stone's experience and recommendations.

---

## Overview

GitHub Copilot Agent Mode + MCP enables:
- Customizable AI assistants for different development phases
- Access to external tools (web search, browser automation, time, etc.)
- Consistent, efficient, and reproducible workflows

---

## Step-by-Step Process / Checklist

### 1. Customization & Setup
- [ ] **Configure VS Code Settings**
    - Enable experimental Copilot features
    - Set custom instructions (`github.copilot.chat.codeGeneration.instructions`)
    - Adjust agent request limits (`chat.agent.maxRequests`)
    - Enable auto-approval for agent tool requests (`chat.tools.autoApprove`)
- [ ] **Install and Configure MCP Servers**
    - Choose relevant MCP servers (e.g., sequentialthinking, searxng, playwright, github, time, fetch)
    - Reference [modelcontextprotocol/servers](https://github.com/modelcontextprotocol/servers) for available options
- [ ] **Create Custom Chat Modes and Prompts**
    - Use VS Code Command Palette to create chat modes for different phases (e.g., research, plan, implement)
    - Store reusable prompts in `.github/prompts/`

---

### 2. Development Workflow

#### Research
- [ ] Switch to a research chat mode (with tools like web search, sequential thinking)
- [ ] Use AI to gather information, analyze results, and synthesize recommendations

#### Planning
- [ ] Switch to a planning chat mode (restrict code editing if desired)
- [ ] Generate a comprehensive `.prompt.md` file in `.github/prompts/` as a blueprint for implementation

#### Implementation
- [ ] Switch to agent mode with full tool access
- [ ] Run the implementation prompt (e.g., `/prompt-name`)
- [ ] Monitor agent progress; pause and provide additional resources if needed
- [ ] If the agent deviates:
    - Clear git diff
    - Update the prompt file
    - Restart implementation

#### Validation
- [ ] Use MCP tools (e.g., playwright) to automate testing and validate output
- [ ] Ensure implementation meets requirements (TDD if possible)

---

## Example Workflow
1. **Research**: Ask Copilot to research a topic using sequentialthinking and web search tools
2. **Planning**: Generate a structured plan and save as a prompt file
3. **Implementation**: Run the prompt to execute the plan
4. **Validation**: Test the output using browser automation or other MCP tools

---

## Benefits
- **Consistency**: Custom instructions ensure code follows your patterns
- **Efficiency**: Pre-planned prompts reduce repetitive explanations
- **Quality**: Structured thinking leads to better decisions
- **Testability**: UI-aware testing tools create comprehensive test suites
- **Reproducibility**: Documented prompts make tasks repeatable

---

## Resources
- [GitHub Copilot Agent Mode](https://code.visualstudio.com/blogs/2025/04/07/agentMode)
- [Model Context Protocol (MCP)](https://modelcontextprotocol.io/introduction)
- [VS Code Copilot Customizations](https://code.visualstudio.com/docs/copilot/copilot-customization)
- [MCP Servers List](https://github.com/modelcontextprotocol/servers)
- [Copilot Chat Modes](https://code.visualstudio.com/docs/copilot/chat/chat-modes)

---

## Tips
- Use the Command Palette (`Ctrl+Shift+P`) to quickly create chat modes and prompts
- Iteratively refine your prompt files for future reuse
- Pause the agent and provide additional context/resources as needed

---

## The Future
Agent Mode + MCP enables context-aware, specialized AI development partners. The more context and structure you provide, the more valuable and consistent the AI's output.

---

*Based on Austen Stone's blog: [Developing with GitHub Copilot Agent Mode and MCP](https://austen.info/blog/github-copilot-agent-mcp/)*
