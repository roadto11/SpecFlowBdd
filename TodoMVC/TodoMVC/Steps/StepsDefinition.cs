﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using TodoMVC.Pages;
using TodoMVC.Steps;

namespace Vicroads.Steps
{
    [Binding]
    class StepsDefinition
    {
        private IWebDriver driver;
        private string AppUrl;
        private HomePage homePage;
        private ToDoPage todoPage;
        private String testData1 = "Testing";
        private String testData2 = "Testing1";

        [Given("I am on the angular page")]
        public void IAmOnTheUVPPage()
        {
           
            AppUrl = ConfigurationSettings.AppSettings["URL"];
            driver = Configuration.Driver;
            driver.Navigate().GoToUrl(AppUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            homePage = new HomePage(driver);
            todoPage = new ToDoPage(driver);


            homePage.ClickOnAngularLink();
        }

        [When(@"I want to add a To-do item")]
        public void GivenIWantToAddATo_DoItem()
        {
            todoPage.EnterToDoName(testData1);
        }

        [When(@"I want to edit the content of an existing To-do item")]
        public void WhenIWantToEditTheContentOfAnExistingTo_DoItem()
        {
            
        }

        [When(@"I can complete a To-do by clicking inside the circle UI to the left of the To-do")]
        public void WhenICanCompleteATo_DoByClickingInsideTheCircleUIToTheLeftOfTheTo_Do()
        {
            todoPage.selectCompleteCircle(1);
            todoPage.clickCompleteFilter();
            todoPage.verifyToDoComplete(testData1);
        }

        [When(@"I can re-activate a completed To-do by clicking inside the circle UI")]
        public void WhenICanRe_ActivateACompletedTo_DoByClickingInsideTheCircleUI()
        {
            todoPage.unSelectCircleButton();
            todoPage.verifyToDoNotFound(testData1);
        }

        [When(@"I can add a second To-do")]
        public void WhenICanAddASecondTo_Do()
        {
            todoPage.clickAllButton();
            todoPage.EnterToDoName(testData2);
        }

        [When(@"I can complete all active To-dos by clicking the down arrow at the top-left of the UI")]
        public void WhenICanCompleteAllActiveTo_DosByClickingTheDownArrowAtTheTop_LeftOfTheUI()
        {
            todoPage.clickAllCheckBox();
        }

        [When(@"I can filter the visible To-dos by Completed state")]
        public void WhenICanFilterTheVisibleTo_DosByCompletedState()
        {   
            todoPage.verifyToDoComplete(testData1);
            todoPage.verifyToDoComplete(testData2);
        }

        [When(@"I can clear a single To-do item from the list completely by clicking the Close icon")]
        public void WhenICanClearASingleTo_DoItemFromTheListCompletelyByClickingTheCloseIcon()
        {          

            todoPage.clickCloseButton();
            todoPage.verifyToDoNotFound(testData1);
        }

        [When(@"I can clear all completed To-do items from the list completely")]
        public void WhenICanClearAllCompletedTo_DoItemsFromTheListCompletely()
        {
            todoPage.clickAllCheckBox();
            todoPage.clickCompleteFilter();
            todoPage.verifyToDoNotFound(testData1);
            todoPage.verifyToDoNotFound(testData2);
        }

    }
}
