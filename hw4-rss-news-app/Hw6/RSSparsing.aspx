<%@ Page Title="Update News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RSSparsing.aspx.cs" Inherits="Hw6.RSSparsing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="row mb-5">
            <div class="col-md-12 text-center">
                <h1 class="fw-bold"><i class="fas fa-sync-alt me-2 text-primary"></i>Update News</h1>
                <p class="text-muted">Fetch the latest news from RSS feeds</p>
            </div>
        </div>

        <!-- RSS Sources + Status (full width) -->
        <div class="row justify-content-center">
            <div class="col-lg-8">
                <!-- RSS Feed Sources -->
                <div class="card shadow-sm mb-4">
                    <div class="card-header bg-primary text-white">
                        <i class="fas fa-rss me-2"></i> RSS Feed Sources
                    </div>
                    <div class="card-body">
                        <ul class="list-group mb-4">
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                <div><i class="fas fa-globe-americas me-2 text-primary"></i><strong>World News</strong></div>
                                <span class="badge bg-primary rounded-pill">HaberTürk</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                <div><i class="fas fa-microchip me-2 text-primary"></i><strong>Technology News</strong></div>
                                <span class="badge bg-primary rounded-pill">HaberTürk</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                <div><i class="fas fa-futbol me-2 text-primary"></i><strong>Sports News</strong></div>
                                <span class="badge bg-primary rounded-pill">HaberTürk</span>
                            </li>
                        </ul>

                        <div class="d-grid">
                            <asp:Button ID="btnParse" runat="server" Text="Fetch Latest News" CssClass="btn btn-primary btn-lg" OnClick="btnParse_Click" />
                        </div>
                    </div>
                </div>

                <!-- Status Panel -->
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <i class="fas fa-info-circle me-2"></i> Status
                    </div>
                    <div class="card-body">
                        <div class="alert alert-info mb-0">
                            <i class="fas fa-info-circle me-2"></i>
                            <asp:Literal ID="ltlResult" runat="server">Ready to parse news. Click "Fetch Latest News" to start.</asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
