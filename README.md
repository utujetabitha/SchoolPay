# SchoolPay API (pair starter)

This is your pair's starting point for SchoolPay. You grow it together across the program, in parallel with your individual ClinicBook:

| Milestone | Tag | Deadline (Kigali time) | What you add | Sandbox? |
|---|---|---|---|---|
| P1 | `p1` | Tue 20 Oct, 23:59 | Students CRUD and invoices, in memory | No |
| P2 | `p2` | Wed 21 Oct, 23:59 | EF Core + SQLite, paging and filters, recorded (cash / bank slip) payments with `Idempotency-Key` | No |
| P3 | `p3` | Sun 25 Oct, 23:59 | JWT, Bursar / Parent roles, ownership, mobile-money payments, signed webhook, SMS receipts, integration tests | Yes |
| Final | `final` | Mon 26 Oct, 12:30 | USSD menu, README, one optional extension | Yes |

The tag must be pushed before the deadline. The grader checks out the tag, not your latest commit.

Read these together before you write code (in the kit):

- `04-pair-project/api-contract.md`: routes, payloads, rules, status codes. The acceptance tests check exactly this.
- `04-pair-project/brief.md`: how the pair works (branches, reviews, tags, grading).

## What is already here

```
SchoolPay.slnx
SchoolPay.Api/
  SchoolPay.Api.csproj             .NET 10, OpenAPI + Scalar
  Program.cs                       controllers, JSON string enums, OpenAPI, Scalar
  Properties/launchSettings.json   http://localhost:5085
  appsettings.json                 "Providers" section for the Provider Sandbox (used from P3)
  SchoolPay.http                   sample P1 calls
  Controllers/InfoController.cs    GET / returns name and version (the tests check it: keep it)
  Models/  Dtos/                   empty folders for your code
  Integrations/                    IMobileMoneyClient, ISmsClient: commented stubs for P3 (Day 3)
.gitignore
README.md                          this file (replace it with your own for the Final)
```

## Set up the shared repo (once per pair)

One partner does this:

```bash
# copy this starter folder somewhere outside the kit, then inside it:
git init -b main
git add .
git commit -m "Start from the SchoolPay starter"
git remote add origin https://github.com/<owner>/schoolpay-<pair-name>.git
git push -u origin main
```

On GitHub: Settings → Collaborators → add your partner and the facilitator. The partner then clones the repo. After this first commit, **nobody pushes to `main` directly**: everything goes through a pull request.

## Required repo layout

The grader looks for exactly these paths. Do not rename or move them.

```
your-pair-repo/
  SchoolPay.slnx
  SchoolPay.Api/SchoolPay.Api.csproj
  SchoolPay.Tests/SchoolPay.Tests.csproj    (from P3)
  README.md
```

Create the test project at P3 from the repo root (on a feature branch):

```bash
dotnet new xunit -n SchoolPay.Tests
dotnet sln SchoolPay.slnx add SchoolPay.Tests/SchoolPay.Tests.csproj
dotnet add SchoolPay.Tests reference SchoolPay.Api
```

## Run the API

From the repo root:

```bash
dotnet run --project SchoolPay.Api
```

Then open:

- API info: http://localhost:5085/
- Scalar UI: http://localhost:5085/scalar
- OpenAPI document: http://localhost:5085/openapi/v1.json

You can also open `SchoolPay.Api/SchoolPay.http` in Visual Studio, VS Code (REST Client) or Rider and send the sample requests.

| App | Port |
|---|---|
| ClinicBook (yours, individual) | 5080 |
| SchoolPay (this API) | 5085 |
| Provider Sandbox (from the kit, P3 and Final) | 5099 |

The grader starts your API like this, after deleting every `*.db` file:

```bash
dotnet run --project SchoolPay.Api --urls http://localhost:5085
```

So from P2 on, your API must create its database on startup, and from P3 seed the bursar account.

## Run the Provider Sandbox (P3 and Final)

The sandbox plays the mobile-money provider, the SMS provider and the USSD aggregator. It needs Node.js 22 and nothing else (no `npm install`). In a separate terminal, from the kit:

```bash
cd 04-pair-project/provider-sandbox
node server.mjs
```

Open http://localhost:5099: phone screen (payment prompts, SMS inbox, `*123#` dialer) and the live Request Log. Your API reaches it with the values in `appsettings.json` → `Providers`. They are public sandbox values; real provider keys never go in this repo (use `dotnet user-secrets` or environment variables such as `Providers__ApiKey`).

## Run the acceptance tests

The tests live in the kit, not in your repo. You need Node.js 22. Keep your API running in one terminal, then in a second terminal:

macOS / Linux (bash, zsh):

```bash
cd 04-pair-project/acceptance-tests
MILESTONE=p1 npm test
```

Windows (PowerShell):

```powershell
cd 04-pair-project/acceptance-tests
$env:MILESTONE="p1"; npm test
```

Change `MILESTONE` to `p2`, `p3` or `final` as you progress. The summary line at the end shows `Passed X / Y`.

Tips:

- The tests start their own Provider Sandbox on port 5099. Stop yours first, or reuse it: `SANDBOX_URL=http://localhost:5099 MILESTONE=p3 npm test` (PowerShell: `$env:SANDBOX_URL="http://localhost:5099"`).
- Restart the API after every code change before you rerun the tests.
- From P2, delete your `*.db` file when you change your models, so the database is recreated.
- Paste the before and after `Passed X / Y` lines into each pull request description.

## Work with branches and pull requests

Every change reaches `main` through a pull request that your partner reviews.

```bash
git switch main && git pull                     # start from the latest main
git switch -c feature/p1-create-student         # one branch per feature or fix
# work, commit small steps
git add .
git commit -m "Add POST /api/students with server-generated studentNumber"
git push -u origin feature/p1-create-student
```

On GitHub: **Compare & pull request** → base `main` → describe what changed and how to try it → ask your partner for a review.

The reviewer uses the checklist in `brief.md`, comments, then approves or requests changes. The author pushes fixes to the same branch. Merge with **Create a merge commit** (keeps both partners' commits visible), then delete the branch. Both of you then update:

```bash
git switch main && git pull
git branch -d feature/p1-create-student
```

Your branch is behind `main` and has conflicts?

```bash
git switch feature/p1-create-student
git merge main          # fix the conflicted files, run the tests
git add . && git commit
git push
```

## Submit a milestone (Git tags)

Each milestone is a Git tag on `main`, pushed to GitHub. One partner tags, after the last pull request of the milestone is merged and the acceptance tests pass on `main`:

```bash
git switch main && git pull
git tag p1 && git push origin p1
```

Use `p1`, `p2`, `p3` and `final`. Tags must be pushed before the deadline.

Made a mistake after tagging? Move the tag (only before the deadline):

```bash
git tag -d p1
git push origin :refs/tags/p1
git tag p1 && git push origin p1
```

## Checklist before you tag

- [ ] `main` contains every merged pull request of the milestone, and no direct pushes
- [ ] `dotnet build` has no errors
- [ ] API starts with `dotnet run --project SchoolPay.Api --urls http://localhost:5085` from a clean clone
- [ ] Acceptance tests pass for your milestone
- [ ] No `bin/`, `obj/`, `*.db` files or real secrets committed
- [ ] Both partners have commits in this milestone
- [ ] (P3) `dotnet test` passes
- [ ] (Final) README explains how you split the work and how you used AI tools

---

Copyright © 2026 Francois Mugorozi @ francoismugorozi@gmail.com
