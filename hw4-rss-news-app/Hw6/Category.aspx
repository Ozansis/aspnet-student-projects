<%@ Page Title="Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="Hw6.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Category Header -->
    <div class="row mb-4">
        <div class="col-md-12">
            <div class="p-4 p-md-5 mb-4 text-white rounded" id="categoryHeader">
                <h1 class="display-5"><%= CategoryName %> News</h1>
                <p class="lead">Explore the latest <%= CategoryName %> news and stay updated with recent developments.</p>
            </div>
        </div>
    </div>

    <!-- Category News Section -->
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="rptCategoryNews" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="news-card card h-100">
                        <div class="position-relative">
                            <img src='<%# !string.IsNullOrEmpty(Eval("ImageUrl").ToString()) ? Eval("ImageUrl") : "https://via.placeholder.com/300x180?text=No+Image" %>' 
                                 class="card-img-top" 
                                 alt='<%# Eval("Title") %>'
                                 onerror="this.src='https://via.placeholder.com/300x180?text=Image+Error'">
                        </div>
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title"><%# Eval("Title") %></h5>
                            <div class="text-muted small mb-2">
                                <i class="far fa-user me-1"></i> <%# Eval("Author") %>
                                <span class="mx-1">|</span>
                                <i class="far fa-calendar-alt me-1"></i> <%# FormatDate(Eval("PubDate").ToString()) %>
                            </div>
                            <a href='NewsDetail.aspx?NewsID=<%# Eval("NewsID") %>' class="btn btn-sm btn-primary mt-auto">Read More</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!-- Boş veri paneli -->
        <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
            <div class="col-12">
                <div class="alert alert-info">
                    <i class="fas fa-info-circle me-2"></i>No news found in this category. Please check back later.
                </div>
            </div>
        </asp:Panel>
    </div>

    <!-- Dynamic styling for category header based on category -->
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const categoryName = '<%= CategoryName %>';
            const headerElement = document.getElementById('categoryHeader');

            switch (categoryName) {
                case 'World':
                    headerElement.classList.add('bg-primary');
                    break;
                case 'Technology':
                    headerElement.classList.add('bg-success');
                    break;
                case 'Sports':
                    headerElement.classList.add('bg-danger');
                    break;
                default:
                    headerElement.classList.add('bg-dark');
                    break;
            }
        });
    </script>
</asp:Content>
