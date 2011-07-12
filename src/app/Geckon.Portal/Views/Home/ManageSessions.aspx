<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteMenu.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Manage Sessions
</asp:Content>

<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent">

<script type="text/javascript">
jQuery(document).ready(function () {
    jQuery("#list").jqGrid({
        url: '/Home/SessionsGrid/',
        datatype: 'json',
        mtype: 'GET',
        colNames: ['SessionID', 'UserID', 'DateCreated','DateModified'],
        colModel: [
          { name: 'SessionID', index: 'SessionID', width: 230, align: 'left' },
          { name: 'UserID', index: 'UserID', width: 60, align: 'left' },
          { name: 'DateCreated', index: 'DateCreated', width: 130, align: 'left'},
          { name: 'DateModified', index: 'DateModified', width: 130, align: 'left'}],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [5, 10, 20, 50],
        sortname: 'SessionID',
        sortorder: "desc",
        viewrecords: true,
        imgpath: '/scripts/themes/coffee/images'
    });
});

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Manage Sessions</h2>
<table id="list"  class="scroll" cellpadding="0" cellspacing="0"></table>
<div   id="pager" class="scroll" style="text-align:center;"></div>

</asp:Content>
