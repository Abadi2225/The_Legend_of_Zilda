# Sprint 0 - Team Repository

This repository contains Sprint 0 implementations from all team members for evaluation.

## Repository Structure

Each team member should create their own folder and add their Sprint 0 work inside it:

```
sprint0-repo/
├── README.md
├── Abdullah/
│   └── sprint0/
│       ├── src/
│       └── ...
└── ...
```

## How to Add Your Sprint 0 Work

### Step 1: Clone the Repository

First, clone this repository to your local machine:

```bash
git clone https://github.com/Abadi2225/Sprint0.git
cd Sprint0
```

### Step 2: Create Your Folder

Create a folder with your name (e.g. Abdullah):

```bash
mkdir your-name
cd your-name
```

### Step 3: Add Your Sprint 0 Folder

Copy or move your sprint0 folder into your personal folder:

**Windows (Command Prompt):**
```cmd
xcopy /E /I C:\path\to\your\sprint0 sprint0
```

**Windows (PowerShell):**
```powershell
Copy-Item -Path C:\path\to\your\sprint0 -Destination sprint0 -Recurse
```

**macOS/Linux:**
```bash
cp -r /path/to/your/sprint0 sprint0
```

## Pushing Your Work to GitHub

### For Windows Users

1. **Navigate to the repository root:**
   ```cmd
   cd C:\path\to\Sprint0
   ```

2. **Add your files:**
   ```cmd
   git add your-name/
   ```

3. **Commit your changes:**
   ```cmd
   git commit -m "Add sprint0 work for [Your Name]"
   ```

4. **Push to GitHub:**
   ```cmd
   git push origin main
   ```

### For macOS Users

1. **Navigate to the repository root:**
   ```bash
   cd /path/to/Sprint0
   ```

2. **Add your files:**
   ```bash
   git add your-name/
   ```

3. **Commit your changes:**
   ```bash
   git commit -m "Add sprint0 work for [Your Name]"
   ```

4. **Push to GitHub:**
   ```bash
   git push origin main
   ```

## Important Notes

- **Replace `your-name`** with your actual name (e.g., `Abdullah`)
- Make sure your sprint0 folder contains all necessary files and documentation
- If you encounter merge conflicts, pull the latest changes first: `git pull origin main`
