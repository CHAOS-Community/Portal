<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Geckon Portal API v3.0
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MenuPanel" runat="server">

<div class="MenuGroup">
    <h2>Manage</h2>
    <ul>
        <li>Users</li>
        <li>Repositories</li>
        <li>Entrypoints</li>
        <li>Modules</li>
    </ul>
</div>

<div class="MenuGroup">
    <h2>Monitor</h2>
    <ul>
        <li>Sessions</li>
        <li>Health</li>
        <li>Logging</li>
    </ul>
</div>

<div class="MenuGroup">
    <h2>View</h2>
    <ul>
        <li>API testing</li>
        <li><a href="http://wiki.geckon.com/index.php/Portal" target="_blank">Documentation</a></li>
    </ul>
</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Index</h2>

</asp:Content>
