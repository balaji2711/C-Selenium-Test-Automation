This Git repository contains a project developed using C#, Selenium, Specflow, Extent Report, and object-oriented programming principles (OOPS). 
The project aims to automate web testing scenarios using the Selenium WebDriver and Specflow, while generating detailed reports using Extent Report.

**To run the tests, you have two options:**

**First Way:**

1. Open the QualiTestAssessment.sln solution file.
2. Navigate to Test -> Test Explorer.
3. Select a specific test and execute it.
4. After the test execution, an Extent Report will be generated at the following path:
> C:/{Environment.MachineName}/{AutomationReport_Timestamp}/index.html

**Note:**

**{Environment.MachineName} = Hostname**

**{AutomationReport_Timestamp} = Timestamp**

**Second Way:**

Batch file available that can execute the tests and generate the LivingDoc report.

1. Locate the bin folder (\QualiTestAssessment\bin\Debug\net6.0).
2. Double-click the Execute Tests.bat file.
3. Wait for the execution to complete.
4. The LivingDoc report will automatically open.

**Extent Report:**

![image](https://github.com/balaji2711/C-Selenium-Test-Automation/assets/15344129/3203409f-666a-4714-941f-7ebf4a969fe4)

**Livingdoc Report:**

![image](https://github.com/balaji2711/C-Selenium-Test-Automation/assets/15344129/baf7a83d-4fc6-4733-87b5-359c8bd39147)
