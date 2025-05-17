<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Hw6.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- Hero Section -->
    <div class="p-4 p-md-5 mb-4 text-white rounded bg-dark">
        <div class="col-md-8 px-0">
            <h1 class="display-4 fst-lightly">Welcome to News Portal</h1>
            <p class="lead my-3">Stay informed with the latest news from around the world. Our portal brings you up-to-date information across various categories including World News, Technology, and Sports.</p>
            <p class="lead mb-0"><a href="#featured-news" class="text-white fw-bold">Continue reading...</a></p>
        </div>
    </div>

    <!-- Featured Categories Section -->
    <div class="row mb-4">
        <div class="col-md-4 mb-3">
            <div class="card bg-primary text-white h-100">
                <div class="card-body d-flex flex-column align-items-center justify-content-center text-center">
                    <i class="fas fa-globe fa-3x mb-3"></i>
                    <h5 class="card-title">World News</h5>
                    <p class="card-text">Stay updated with international events and global news coverage.</p>
                    <a href="Category.aspx?Category=World" class="btn btn-light mt-auto">View World News</a>
                </div>
            </div>
        </div>
        <div class="col-md-4 mb-3">
            <div class="card bg-success text-white h-100">
                <div class="card-body d-flex flex-column align-items-center justify-content-center text-center">
                    <i class="fas fa-microchip fa-3x mb-3"></i>
                    <h5 class="card-title">Technology</h5>
                    <p class="card-text">Discover the latest advancements and innovations in technology.</p>
                    <a href="Category.aspx?Category=Technology" class="btn btn-light mt-auto">View Technology News</a>
                </div>
            </div>
        </div>
        <div class="col-md-4 mb-3">
            <div class="card bg-danger text-white h-100">
                <div class="card-body d-flex flex-column align-items-center justify-content-center text-center">
                    <i class="fas fa-futbol fa-3x mb-3"></i>
                    <h5 class="card-title">Sports</h5>
                    <p class="card-text">Follow the latest sports events, matches, and athlete updates.</p>
                    <a href="Category.aspx?Category=Sports" class="btn btn-light mt-auto">View Sports News</a>
                </div>
            </div>
        </div>
    </div>

    <!-- Latest News Section -->
    <section id="featured-news">
        <h2 class="section-title">Latest News</h2>
        
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater ID="rptAllNews" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="news-card card h-100">
                            <div class="position-relative">
                                <img src='<%# !string.IsNullOrEmpty(Eval("ImageUrl").ToString()) ? Eval("ImageUrl") : "https://via.placeholder.com/300x180?text=No+Image" %>' 
                                     class="card-img-top" 
                                     alt='<%# Eval("Title") %>' 
                                     onerror="this.src='https://via.placeholder.com/300x180?text=Image+Error'">
                                <span class="position-absolute top-0 end-0 category-badge m-2">
                                    <%# Eval("Category") %>
                                </span>
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
        </div>
    </section>

    <!-- Subscribe Section -->
    <div class="p-4 p-md-5 mt-5 text-white rounded bg-secondary">
        <div class="row align-items-center">
            <div class="col-md-8">
                <h3>Subscribe to our Newsletter</h3>
                <p>Get the latest news delivered directly to your inbox</p>
            </div>
            <div class="col-md-4">
                <div class="input-group">
                    <input type="email" class="form-control" placeholder="Your Email" aria-label="Email" aria-describedby="subscribe-btn">
                    <button class="btn btn-primary" type="button" id="subscribe-btn">Subscribe</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>