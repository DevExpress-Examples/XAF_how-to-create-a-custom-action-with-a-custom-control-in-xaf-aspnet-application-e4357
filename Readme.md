# How to create a custom Action with a custom control in XAF ASP.NET application


<p><strong>Scenario</strong><br>Sometimes, it is necessary to extend our built-in Actions to be able to use a custom control for a specific task. For example, add two DateTime editors to accept a data range instead of creating two separate ParametrizedActions.</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-create-a-custom-action-with-a-custom-control-in-xaf-aspnet-application-e4357/12.1.8+/media/dfa5b301-c102-11e6-80bf-00155d62480c.png"><br><br></p>
<p><strong>Steps to implement:<br></strong></p>
<p><strong>16.1.7+</strong><br>Inherit the <a href="https://documentation.devexpress.com/#eXpressAppFramework/clsDevExpressExpressAppWebSystemModuleProcessActionContainerHolderControllertopic">ProcessActionContainerHolderController</a> and override the OnCreateCustomMenuActionItem method and return a custom TemplatedMenuActionItem, which will represent the Action's control. See the MyProcessActionContainerHolderController.xx class code for additional details.<br><br><strong>Prior to version 16.1.7<br></strong>It is required to place a custom ActionContainerHolder to our web templates (aspx pages) instead of the default ActionContainerHolder, otherwise, a custom Action item will not be created. See how this is done in the DefaultTemplateContent1.ascx file. For additional information, refer to the <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113460.aspx">How to: Customize an ASP.NET Template</a> topic.</p>

<br/>


