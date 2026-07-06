# QA Automation Skill Development - Learning Design

**Epic:** [SDP-37389 - Nikolay Dimitrov - QA Automation Skill Development (C# Track)](https://draftkingsofficial.atlassian.net/browse/SDP-37389)
**Author:** Nikolay Dimitrov
**Date:** 2026-07-06
**Duration:** ~3-4 months (leave: 2026-07-06 → 2026-08-16 heavy focus, then part-time)

## Purpose

Convert the 7 backlog tasks under epic SDP-37389 from theoretical checkboxes into a
structured, evidence-driven learning delivery that:

1. Refreshes rusty coding skills (Java background, ~years gap).
2. Builds real C# / test-automation competence usable on Apollo team services.
3. Produces visible progress in Jira for the QA Lead: closed tickets, commits, PR-links.

## Starting Point

- Java OOP background (theory solid; not code-active recently).
- LINQ, lambdas, DI are new.
- Existing SoftUni Fundamentals exercises on disk (loops/conditionals/small programs) -
  reference material, not primary source-of-truth.
- Familiarity with Apollo services from manual QA work: BetGenius, BG Ably,
  BG LiveData, BG Parser, OddsJam Distributor.

## Approach: Learning-first, Sequential (with parallel PR reviews)

Chosen over Apollo-first-vertical-slice and parallel-tracks approaches.
Rationale: build syntax confidence in isolation before entering a real repo with
existing conventions. Kafka is a "cheap win" in the middle (documentation-heavy).
PR reviews (SDP-37396) run opportunistically once C# foundations exist.

## Learning Delivery Model

Each of the 7 tasks produces **3 required artefacts**:

1. **Working code** in personal repo `qa-automation-learning` (private GitHub,
   folder-per-task).
2. **Task notes** (`notes.md` in the task folder): what was covered, key learnings,
   open questions.
3. **Jira update**: preview-approved comment linking commits + AC coverage +
   status transition (Backlog → In Progress → Done).

Kafka (SDP-37393) and Local Setup (SDP-37394) are documentation-heavy tasks: markdown
+ screenshots instead of large code deliverables.

Real Automated Tests (SDP-37395) and PR Reviews (SDP-37396) produce artefacts in
Apollo repos, not in the personal repo. The personal repo keeps an
`evidence-links.md` pointing to those PRs.

## Personal Repo Structure

```
qa-automation-learning/                      # Private GitHub, .NET 8+
├── README.md
├── .gitignore                               # VisualStudio template
├── 01-csharp-fundamentals/                  # SDP-37390
│   ├── CSharpFundamentals.sln
│   ├── CSharpFundamentals/
│   │   ├── Program.cs
│   │   ├── Models/                          # OOP demo
│   │   ├── Services/                        # DI demo
│   │   └── LinqDemo.cs
│   └── notes.md
├── 02-restsharp-api/                        # SDP-37391 + SDP-37392 (same folder,
│   ├── RestfulBookerTests.sln               #   separate commits for evidence)
│   ├── RestfulBookerTests/
│   │   ├── Tests/
│   │   ├── Models/
│   │   └── Clients/
│   └── notes.md
├── 03-kafka-offset-explorer/                # SDP-37393
│   ├── apollo-topics-inventory.md
│   ├── sample-payloads/
│   ├── screenshots/
│   └── troubleshooting-guide.md
├── 04-apollo-local-setup/                   # SDP-37394
│   ├── setup-log.md
│   ├── debugging-walkthrough.md
│   └── screenshots/
├── docs/
│   ├── superpowers/specs/                   # design docs (this file)
│   └── evidence-links.md                    # links to Apollo PRs (SDP-37395/96)
```

Note: SDP-37391 and SDP-37392 share `02-restsharp-api/`. SDP-37391 lands as the
initial API-test commits; SDP-37392 lands as a follow-up refactor commit
replacing NUnit `Assert.*` with FluentAssertions.

## Phased Timeline

Assumes ~1-2h/weekday + 2-3h one weekend day → ~10-13h/week during leave,
~5-8h/week after leave.

### Phase 1 - Fundamentals (~2-3 weeks)

Runs during leave (2026-07-07 onwards). Goal: rebuild coding confidence.

| Task | Est. | Deliverable |
|------|------|-------------|
| **SDP-37390** C# Fundamentals & OOP | 6-8h | Console project: syntax basics + OOP hierarchy + LINQ demo + DI demo |
| **SDP-37391** API Automation (RestSharp) | 4-6h | NUnit test project against Restful Booker API |
| **SDP-37392** FluentAssertions | 2-3h | Refactor SDP-37391 tests to fluent syntax |

### Phase 2 - Apollo Context (~1-1.5 weeks)

Late leave / start of return to work.

| Task | Est. | Deliverable |
|------|------|-------------|
| **SDP-37393** Offset Explorer / Kafka | 2-3h | Apollo topics inventory + sample payloads + troubleshooting guide |
| **SDP-37394** Local Apollo setup + debug | 4-6h | Working local Rancher/Tilt stack + debugging walkthrough |

### Phase 3 - Real Contribution (2-3 weeks, in parallel)

Runs post-leave, in working hours.

| Task | Est. | Deliverable |
|------|------|-------------|
| **SDP-37395** Real automated tests + PR | 10-15h | 2+ NUnit/Reqnroll tests merged (or in review) into an Apollo automation repo |
| **SDP-37396** PR Reviews | 3-5h | 3-5 substantive PR reviews in Apollo repos, at least one with accepted feedback |

**SDP-37396 starts opportunistically as soon as SDP-37390 is done.** Not blocked
by SDP-37395.

## Per-Task Working Cycle

Every task runs through the same cycle:

1. **Kickoff** - pull AC from Jira; break into sub-steps; transition Backlog → In Progress.
2. **Guided coding sessions** - user writes code; Claude provides Java-analog explanations,
   small step-by-step prompts, code review questions, refactor suggestions.
3. **Verification** - walk through Jira AC checklist item-by-item; confirm each with evidence.
4. **Evidence packaging** - descriptive commits, `notes.md` update, draft Jira comment.
5. **Preview + publish** - user approves the drafted comment; Claude publishes it and
   transitions the ticket status.

**Non-negotiables:**

- No Jira comment posted without explicit user approval.
- No ticket status change without confirmation.
- Every AC bullet is linked to a specific artefact (commit, file, screenshot).

## Evidence Comment Template

Used for every task closure in Jira:

```
h3. SDP-37XXX Completion Evidence

*Repo:* [qa-automation-learning/0X-folder|<url>]
*Commits:*
- <hash> — <one-liner>
- <hash> — <one-liner>

*What I covered (mapped to Acceptance Criteria):*
* AC-1: <what I did> — see <file:line> or <commit>
* AC-2: <what I did> — see <file:line> or <commit>
* AC-3: partial — <what's missing>, plan to address in <next task>

*Key learnings:*
* <2-3 bullets>

*Open questions for QA Lead:*
* <or: "None">

Ready for review.
```

## SoftUni Materials Reuse

Existing SoftUni exercises on disk (mostly .NET Framework 4.7.2/4.8, some .NET 8)
are used as **reference and refresher fodder**, not copied wholesale:

- Pick 2-3 representative exercises (e.g., `SumOfChars`, `PetShop`, `PasswordGuess`).
- Port to .NET 8 in `01-csharp-fundamentals/` with modern C# (top-level statements,
  file-scoped namespaces).
- Extend one of them (e.g., `PetShop`) into a mini OOP hierarchy demo.
- Reduces Sub-session A time from ~1.5-2h to ~1h.

OOP, LINQ, and DI are written fresh - SoftUni Fundamentals doesn't cover them and
Java OOP knowledge is enough context for a C#-specific refresh.

## Tooling

- **IDE:** Rider (JetBrains) for personal repo; **Visual Studio Community** additionally
  for SDP-37394 (ticket explicitly names VS).
- **Git host:** private GitHub repo under user's personal account.
- **Local infra:** Rancher Desktop + Tilt (Phase 2).
- **Framework stack:** .NET 8, NUnit, Reqnroll, RestSharp, FluentAssertions,
  Microsoft.Extensions.DependencyInjection.

## Success Criteria

- All 7 tasks transitioned Backlog → Done in Jira with evidence comments.
- Personal repo public commit log demonstrates progression from syntax → OOP → API
  tests → framework integration.
- At least 1 PR merged into an Apollo automation repo (SDP-37395).
- At least 1 PR review comment accepted or discussed with author (SDP-37396).
- QA Lead can, at any point, look at Jira + repo and see current progress without
  asking the engineer.

## What This Design Explicitly Does NOT Do

- **No pre-built solutions handed over.** User writes every line; Claude guides,
  reviews, and refactors.
- **No CI/CD pipeline authoring** beyond what Apollo repos already have. The AC in
  SDP-37389 says "basic understanding of CI/CD integration" - satisfied by walking
  through the existing pipeline in Phase 3.
- **No Reqnroll deep-dive** as its own phase. Reqnroll usage lands naturally in
  SDP-37395 when contributing to an Apollo repo that uses it.
- **No cross-Apollo-service comparison.** One Apollo service is chosen at the start
  of Phase 2 (candidate: BetGenius Ably Distributor, given the engineer already has
  QA architecture knowledge; final decision after inspecting which repo has the most
  approachable existing automation framework).

## Open Items to Decide at Phase Boundaries

- **Start of Phase 2:** which Apollo service becomes "the" service for SDP-37394/95.
- **Start of Phase 3:** whether Reqnroll is used for the new tests in SDP-37395 or
  plain NUnit is more appropriate given the specific Apollo repo's conventions.

## Transition to Implementation Plan

Next step: invoke `writing-plans` to produce a task-by-task implementation plan
with concrete sub-steps, checkpoints, and mentor/QA-Lead touchpoints.
