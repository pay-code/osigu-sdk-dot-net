﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.0.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace OsiguSDK.SpecificationTests.Settlements.Tests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Create Multiple Claims In Different Providers")]
    public partial class CreateMultipleClaimsInDifferentProvidersFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CreateMultipleClaimsInDifferentProviders.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Create Multiple Claims In Different Providers", "\tIn order to create multiple claims for a settlement \r\n\tAs Provider\r\n\tI want to b" +
                    "e able to create multiple claims", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create Claim Of A Provider")]
        [NUnit.Framework.TestCaseAttribute("1", "dev1", "dev1", "5", "1,2,3", new string[0])]
        [NUnit.Framework.TestCaseAttribute("2", "dev2", "dev2", "3", "1,2,3", new string[0])]
        [NUnit.Framework.TestCaseAttribute("3", "dev3", "dev3", "4", "1,2,3", new string[0])]
        public virtual void CreateClaimOfAProvider(string testId, string slug, string token, string claimQuantity, string productIds, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create Claim Of A Provider", exampleTags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "TestId",
                        "Slug",
                        "Token",
                        "ClaimQuantity",
                        "ProductIds"});
            table1.AddRow(new string[] {
                        string.Format("{0}", testId),
                        string.Format("{0}", slug),
                        string.Format("{0}", token),
                        string.Format("{0}", claimQuantity),
                        string.Format("{0}", productIds)});
#line 8
 testRunner.Given("I have the provider selected claims client", ((string)(null)), table1, "Given ");
#line 11
 testRunner.And("I have the insurer authorizations client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("I have the queue client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.When("I complete the claim creation process", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
