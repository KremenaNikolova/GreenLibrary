import NavBar from "./components/NavBar";
import Home from "./pages/Home";
import CreateArticle from "./pages/CreateArticleForm";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import ArticleDetails from "./pages/ArticleDetails";
import CategoryArticlesList from "./pages/CategoryArticlesList";
import LoginForm from "./pages/LoginForm";
import RegisterForm from "./pages/RegisterForm";
import SearchArticles from "./pages/SearchArticles";
import Articles from "./pages/Articles";
import ApprovalArticlesAdmin from "./pages/ApprovalArticlesAdmin";
import ForgotPasswordForm from "./pages/ForgotPasswordForm";
import UserProfilePage from "./pages/UserProfilePage";
import Users from "./pages/Users";
import UserSettings from "./pages/UserSettings";


export default function App() {

    return (
        <Router>
            <NavBar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/articles/:id" element={<ArticleDetails />} />
                <Route path="/create" element={<CreateArticle />} />
                <Route path="/categories/:category" element={<CategoryArticlesList />} />
                <Route path="/login" element={<LoginForm />} />
                <Route path="/register" element={<RegisterForm />} />
                <Route path="/search" element={<SearchArticles />} />
                <Route path="/articles" element={<Articles />} />
                <Route path="/user/settings" element={<UserSettings />} />
                <Route path="/user/:userId" element={<UserProfilePage />} />
                <Route path="/admin/articles" element={<ApprovalArticlesAdmin />} />
                <Route path="/admin/users" element={<Users />} />
                <Route path="/email/reset-password" element={<ForgotPasswordForm /> } />
            </Routes>
        </Router>
    );
}
