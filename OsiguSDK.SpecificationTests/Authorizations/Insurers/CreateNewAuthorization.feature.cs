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
namespace OsiguSDK.SpecificationTests.Authorizations.Insurers
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Create a new authorization as an Insurer")]
    public partial class CreateANewAuthorizationAsAnInsurerFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CreateNewAuthorization.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Create a new authorization as an Insurer", "\tIn order to create an authorization\r\n\tAs an Insurer \r\n\tI need to send all the ne" +
                    "ccesary data of the authorization", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Create a new authorization with an invalid token")]
        public virtual void CreateANewAuthorizationWithAnInvalidToken()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new authorization with an invalid token", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("I have the insurer authorizations client with an invalid token", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.And("I have the request data for a new authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
 testRunner.When("I make the new authorization request to the endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("the result should be forbidden for that request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a new authorization with an invalid slug")]
        public virtual void CreateANewAuthorizationWithAnInvalidSlug()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new authorization with an invalid slug", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
 testRunner.Given("I have the insurer authorizations client with an invalid slug", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
 testRunner.And("I have the request data for a new authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.When("I make the new authorization request to the endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.Then("the result should be unauthorized for that request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a new authorization with empty fields")]
        public virtual void CreateANewAuthorizationWithEmptyFields()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new authorization with empty fields", ((string[])(null)));
#line 18
this.ScenarioSetup(scenarioInfo);
#line 19
 testRunner.Given("I have the insurer authorizations client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
 testRunner.And("I have the request data for a new authorization with empty fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.When("I make the new authorization request to the endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 22
 testRunner.Then("the result should be unprocessable fot that request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a new authorization with an invalid reference_id")]
        public virtual void CreateANewAuthorizationWithAnInvalidReference_Id()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new authorization with an invalid reference_id", ((string[])(null)));
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("I have the insurer authorizations client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
 testRunner.And("I have the request data for a new authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.When("I make the new authorization request to the endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then("I have valid response for creating the authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 29
 testRunner.When("I make the new authorization request to the endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("the result should be unprocessable fot that request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a new authorization with an unreferenced product")]
        public virtual void CreateANewAuthorizationWithAnUnreferencedProduct()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new authorization with an unreferenced product", ((string[])(null)));
#line 32
this.ScenarioSetup(scenarioInfo);
#line 33
 testRunner.Given("I have the insurer authorizations client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 34
 testRunner.And("I have the request data for a new authorization with an unreferenced product", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.When("I make the new authorization request to the endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.Then("the result should be unprocessable fot that request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a new authorization with a duplicate product")]
        public virtual void CreateANewAuthorizationWithADuplicateProduct()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new authorization with a duplicate product", ((string[])(null)));
#line 38
this.ScenarioSetup(scenarioInfo);
#line 39
 testRunner.Given("I have the insurer authorizations client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
 testRunner.And("I have the request data for a new authorization with a duplicate product", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.When("I make the new authorization request to the endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 42
 testRunner.Then("the result should be unprocessable fot that request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a new valid authorization")]
        public virtual void CreateANewValidAuthorization()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new valid authorization", ((string[])(null)));
#line 44
this.ScenarioSetup(scenarioInfo);
#line 45
 testRunner.Given("I have the insurer authorizations client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 46
 testRunner.And("I have the request data for a new authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
 testRunner.When("I make the new authorization request to the endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 48
 testRunner.Then("I have valid response for creating the authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
