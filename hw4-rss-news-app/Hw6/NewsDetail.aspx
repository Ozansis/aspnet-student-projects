<%@ Page Title="News Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="Hw6.NewsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .news-detail-container {
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0,0,0,0.05);
            overflow: hidden;
            margin-top: 30px;
        }

        .news-header {
            position: relative;
            height: 350px;
            overflow: hidden;
        }

        .news-header img {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }

        .news-overlay {
            position: absolute;
            bottom: 0;
            left: 0;
            right: 0;
            padding: 40px 20px 20px;
            background: linear-gradient(to top, rgba(0,0,0,0.8), rgba(0,0,0,0));
            color: white;
        }

        .news-category {
            display: inline-block;
            background-color: #007bff;
            color: white;
            padding: 5px 15px;
            border-radius: 30px;
            font-size: 0.85rem;
            margin-bottom: 10px;
        }

        .news-meta {
            margin-top: 15px;
            padding-bottom: 15px;
            border-bottom: 1px solid rgba(0,0,0,0.1);
        }

        .news-content {
            font-size: 1.1rem;
            line-height: 1.8;
        }

        .breadcrumb {
            background-color: transparent;
            padding-left: 0;
            margin-top: 20px;
        }

        .breadcrumb-item + .breadcrumb-item::before {
            content: ">";
            padding: 0 5px;
        }

        .share-buttons {
            margin-top: 20px;
        }

        .share-buttons a {
            display: inline-block;
            width: 40px;
            height: 40px;
            line-height: 40px;
            text-align: center;
            border-radius: 50%;
            color: white;
            margin-right: 8px;
            transition: transform 0.3s;
        }

        .share-buttons a:hover {
            transform: translateY(-3px);
        }

        .facebook { background-color: #3b5998; }
        .twitter { background-color: #1da1f2; }
        .linkedin { background-color: #0077b5; }
        .whatsapp { background-color: #25d366; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container news-detail-container">
        <div class="news-header">
            <asp:Image ID="imgNews" runat="server" AlternateText="News Image" />
            <div class="news-overlay">
                <span class="news-category">
                    <asp:Literal ID="ltCategoryBadge" runat="server" />
                </span>
                <h1><asp:Literal ID="ltTitle" runat="server" /></h1>
            </div>
        </div>

        <div class="p-4">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx">Home</a></li>
                    <li class="breadcrumb-item">
                        <a href="Category.aspx?cat=<%= ltCategoryNav.Text %>">
                            <asp:Literal ID="ltCategoryNav" runat="server" />
                        </a>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">
                        <asp:Literal ID="Literal1" runat="server" />
                    </li>
                </ol>
            </nav>

            <div class="news-meta mb-3">
                <p><strong>Author:</strong> <asp:Literal ID="ltAuthor" runat="server" /></p>
                <p><strong>Published:</strong> <asp:Literal ID="ltPubDate" runat="server" /></p>
                <p><strong>Category:</strong> <asp:Literal ID="ltCategory" runat="server" /></p>
            </div>

            <div class="news-content">
                <p><asp:Literal ID="ltDescription" runat="server" /></p>
            </div>

            <div class="mt-3">
                <strong>Category Link:</strong>
                <asp:Literal ID="ltCategoryLink" runat="server" />
            </div>

            <div class="share-buttons mt-4">
                <a href="#" class="facebook" title="Share on Facebook"><i class="fab fa-facebook-f"></i></a>
                <a href="#" class="twitter" title="Share on Twitter"><i class="fab fa-twitter"></i></a>
                <a href="#" class="linkedin" title="Share on LinkedIn"><i class="fab fa-linkedin-in"></i></a>
                <a href="#" class="whatsapp" title="Share on WhatsApp"><i class="fab fa-whatsapp"></i></a>
            </div>
        </div>
    </div>
</asp:Content>
