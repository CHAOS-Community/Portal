<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteMenu.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Manage Sessions
</asp:Content>

<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent">

<link href="/Grapthics/smoothness/jquery-ui-1.8.14.custom.css" type="text/css" rel="stylesheet" />
<link href="/Grapthics/smoothness/ui.jqgrid.css" type="text/css" rel="stylesheet" />

<script src="/Scripts/i18n/grid.locale-en.js" type="text/javascript"></script>
<script src="/Scripts/jquery.jqGrid.min.js" type="text/javascript"></script>

<script type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("#list").jqGrid({
            url: '/Home/SessionsGrid/',
            datatype: 'json',
            mtype: 'GET',
            colNames: ['SessionID', 'UserID', 'ClientSettingID', 'DateCreated', 'DateModified'],
            colModel: [
          { name: 'SessionID', index: 'SessionID', width: 250, align: 'left' },
          { name: 'UserID', index: 'UserID', width: 60, align: 'left' },
          { name: 'ClientSettingID', index: 'ClientSettingID', width: 90, align: 'left' },
          { name: 'DateCreated', index: 'DateCreated', width: 130, align: 'left' },
          { name: 'DateModified', index: 'DateModified', width: 130, align: 'left'}],
            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [5, 10, 20, 50],
            sortname: 'SessionID',
            sortorder: "desc",
            viewrecords: true,
            caption: '',
            imgpath: '/Graphics/smoothness/images'
        });

        jQuery("#list").setGridHeight(220, true);
    });

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Manage Sessions</h2>
<table id="list"  class="scroll" cellpadding="0" cellspacing="0"></table>
<div   id="pager" class="scroll" style="text-align:center;"></div>

</asp:Content>
