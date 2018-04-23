<%@ Control Language="C#" CodeBehind="DefaultTemplateContent1.ascx.cs" ClassName="DefaultTemplateContent1" Inherits="Solution28.Web.DefaultTemplateContent1"
    CompilationMode="Auto" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxRoundPanel"
    TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxPanel"
    TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxSplitter"
    TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxGlobalEvents"
    TagPrefix="dxge" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v14.1" Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers"
    TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v14.1" Namespace="DevExpress.ExpressApp.Web.Templates"
    TagPrefix="cc3" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v14.1" Namespace="DevExpress.ExpressApp.Web.Controls"
    TagPrefix="cc4" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v14.1" Namespace="DevExpress.ExpressApp.Web.Templates.Controls"
    TagPrefix="tc" %>
<%@ Register Assembly="Solution28.Module.Web" Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers" TagPrefix="ccmy" %>
<div class="HorizontalTemplate BodyBackColor">
    <dxge:ASPxGlobalEvents ID="GE" ClientInstanceName="GE" ClientSideEvents-EndCallback="AdjustSize"
        runat="server" />
    <table id="MT" border="0" width="100%" cellpadding="0" cellspacing="0" class="dxsplControl_<%= BaseXafPage.CurrentTheme %>">
        <tbody>
            <tr>
                <td style="vertical-align: top; height: 10px;" class="dxsplPane_<%= BaseXafPage.CurrentTheme %>">
                    <div id="HorizontalTemplateHeader" class="HorizontalTemplateHeader" style="width: 100%;">
                        <table cellpadding="0" cellspacing="0" border="0" class="Top" width="100%">
                            <tr>
                                <td class="Logo">
                                    <asp:HyperLink runat="server" NavigateUrl="#" ID="LogoLink">
                                        <cc4:ThemedImageControl ID="TIC" ImageName="Logo.png" DefaultThemeImageLocation="App_Themes/{0}/Xaf"
                                            runat="server" BorderWidth="0px" />
                                    </asp:HyperLink>
                                </td>
                                <td class="Security">
                                    <cc3:XafUpdatePanel ID="UPSAC" runat="server">
                                        <ccmy:MyActionContainerHolder runat="server" ID="SAC" Categories="Security" ContainerStyle="Links"
                                            PaintStyle="CaptionAndImage" CssClass="Security" SeparatorHeight="23px" ShowSeparators="True" />
                                    </cc3:XafUpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <cc3:XafUpdatePanel ID="UPNTAC" runat="server">
                            <cc2:NavigationTabsActionContainer ID="NTAC" runat="server" ContainerId="ViewsNavigation"
                                CssClass="NavigationTabsActionContainer">
                                <spaceaftertabstemplate>
                                    <ccmy:MyActionContainerHolder ID="VN" runat="server" Categories="RootObjectsCreation;Appearance;Search;FullTextSearch"
                                        ContainerStyle="Links" CssClass="TabsContainer" SeparatorHeight="15px" />
                                </spaceaftertabstemplate>
                            </cc2:NavigationTabsActionContainer>
                        </cc3:XafUpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <table id="MRC" style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td id="LPcell" style="width: 200px; vertical-align: top;">
                                <div id="LP" class="LeftPane" style="width: 200px;">
                                    <cc3:XafUpdatePanel ID="UPLP" runat="server">
                                        <ccmy:MyActionContainerHolder ID="VTC" runat="server" Orientation="Vertical" Categories="Tools"
                                            BorderWidth="0px" ContainerStyle="Links" ShowSeparators="False" />
                                        <ccmy:MyActionContainerHolder ID="DAC" runat="server" Orientation="Vertical" Categories="Diagnostic"
                                            BorderWidth="0px" ContainerStyle="Links" ShowSeparators="False" />
                                        <br />
                                    </cc3:XafUpdatePanel>
                                </div>
                            </td>
                            <td id="separatorCell" style="width: 6px; border-bottom-style: none; border-top-style: none;
                                display: none" class="dxsplVSeparator_<%= BaseXafPage.CurrentTheme %> dxsplPane_<%= BaseXafPage.CurrentTheme %>">
                                <div id="separatorButton" class="dxsplVSeparatorButton_<%= BaseXafPage.CurrentTheme %>" onmouseover="OnMouseEnter('separatorButton')"
                                    onmouseout="OnMouseLeave('separatorButton')" onclick="OnClick('LPcell', 'separatorImage', true)">
                                    <div id="separatorImage" style="width: 6px;" class="dxWeb_splVCollapseBackwardButton_<%= BaseXafPage.CurrentTheme %>">
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: top;">
                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <cc3:XafUpdatePanel ID="UPTB" runat="server">
                                                    <ccmy:MyActionContainerHolder CssClass="ACH MainToolbar" runat="server" ID="TB" ContainerStyle="ToolBar"
                                                        Orientation="Horizontal" Categories="ObjectsCreation;Edit;RecordEdit;View;Export;Reports;Filters">
                                                        <menu width="100%" itemautowidth="False" clientinstancename="mainMenu">
                                                            <bordertop borderstyle="None" />
                                                            <borderleft borderstyle="None" />
                                                            <borderright borderstyle="None" />
                                                        </menu>
                                                    </ccmy:MyActionContainerHolder>
                                                </cc3:XafUpdatePanel>
                                                <cc3:XafUpdatePanel ID="UPVH" runat="server">
                                                    <table id="VH" border="0" cellpadding="0" cellspacing="0" class="MainContent" width="100%">
                                                        <tr>
                                                            <td class="ViewHeader">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="ViewHeader">
                                                                    <tr>
                                                                        <td class="ViewImage">
                                                                            <cc4:ViewImageControl ID="VIC" runat="server" />
                                                                        </td>
                                                                        <td class="ViewCaption">
                                                                            <h1>
                                                                                <cc4:ViewCaptionControl ID="VCC" runat="server" />
                                                                            </h1>
                                                                            <cc2:NavigationHistoryActionContainer ID="VHC" runat="server" CssClass="NavigationHistoryLinks"
                                                                                ContainerId="ViewsHistoryNavigation" Delimiter=" / " />
                                                                        </td>
                                                                        <td align="right">
                                                                            <ccmy:MyActionContainerHolder runat="server" ID="RNC" ContainerStyle="Links" Orientation="Horizontal"
                                                                                Categories="RecordsNavigation" UseLargeImage="True" PaintStyle="Image" CssClass="RecordsNavigationContainer">
                                                                                <menu width="100%" itemautowidth="False" horizontalalign="Right" />
                                                                            </ccmy:MyActionContainerHolder>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </cc3:XafUpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cc3:XafUpdatePanel ID="UPEMA" runat="server">
                                                    <ccmy:MyActionContainerHolder runat="server" ID="EMA" ContainerStyle="Links" Orientation="Horizontal"
                                                        Categories="Save;UndoRedo" CssClass="EditModeActions">
                                                        <menu width="100%" itemautowidth="False" horizontalalign="Right" />
                                                    </ccmy:MyActionContainerHolder>
                                                </cc3:XafUpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="CP" style="overflow: auto; width: 100%;">
                                                    <table border="0" cellpadding="0" cellspacing="0" class="MainContent" width="100%">
                                                        <tr class="Content">
                                                            <td class="Content">
                                                                <cc3:XafUpdatePanel ID="UPEI" runat="server">
                                                                    <tc:ErrorInfoControl ID="ErrorInfo" Style="margin: 10px 0px 10px 0px" runat="server">
                                                                    </tc:ErrorInfoControl>
                                                                </cc3:XafUpdatePanel>
                                                                <cc3:XafUpdatePanel ID="UPVSC" runat="server">
                                                                    <cc4:ViewSiteControl ID="VSC" runat="server" />
                                                                    <ccmy:MyActionContainerHolder runat="server" ID="EditModeActions2" ContainerStyle="Links"
                                                                        Orientation="Horizontal" Categories="Save;UndoRedo" CssClass="EditModeActions">
                                                                        <menu width="100%" itemautowidth="False" horizontalalign="Right" paddings-paddingtop="15px" />
                                                                    </ccmy:MyActionContainerHolder>
                                                                </cc3:XafUpdatePanel>
                                                                <div id="Spacer" class="Spacer">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="Content">
                                                            <td class="Content Links" align="center" style="text-align: center">
                                                                <span style="display: inline-block">
                                                                    <cc3:XafUpdatePanel ID="UPQC" runat="server">
                                                                        <cc2:QuickAccessNavigationActionContainer CssClass="NavigationLinks" ID="QC" runat="server"
                                                                            ContainerId="ViewsNavigation" PaintStyle="Caption" ShowSeparators="True" />
                                                                    </cc3:XafUpdatePanel>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 20px; vertical-align: bottom" class="BodyBackColor">
                    <cc3:XafUpdatePanel ID="UPIMP" runat="server">
                        <asp:Literal ID="InfoMessagesPanel" runat="server" Text="" Visible="False"></asp:Literal>
                    </cc3:XafUpdatePanel>
                    <div id="Footer" class="Footer">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left">
                                    <div class="FooterCopyright">
                                        <cc4:AboutInfoControl ID="AIC" runat="server">Copyright text</cc4:AboutInfoControl>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
