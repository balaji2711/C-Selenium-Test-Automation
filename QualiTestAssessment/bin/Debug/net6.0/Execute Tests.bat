set DIR=%~dp0
chdir /d %DIR% 
NUnit-3.15.4\bin\net6.0\nunit3-console.exe QualiTestAssessment.dll --where "cat == Regression" 

Timeout 60

set DIR=%~dp0
chdir /d %DIR% 

livingdoc test-assembly QualiTestAssessment.dll -t TestExecution.json

LivingDoc.html