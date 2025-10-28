import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';
import './Header.css';

const Header: React.FC = () => {
  const { user, isAuthenticated, logout } = useAuth();

  return (
    <header className="header">
      <div className="container">
        <div className="header-content">
          <Link to="/" className="logo">
            <h1>🎮 GamePlatform</h1>
          </Link>
          
          <nav className="nav">
            <Link to="/">Home</Link>
            <Link to="/games">Games</Link>
            <Link to="/categories">Categories</Link>
            
            {isAuthenticated ? (
              <>
                {user?.isAdmin && <Link to="/admin">Admin</Link>}
                <span className="user-info">Hi, {user?.username}!</span>
                <button onClick={logout} className="btn-logout">Logout</button>
              </>
            ) : (
              <>
                <Link to="/login">Login</Link>
                <Link to="/register">Register</Link>
              </>
            )}
          </nav>
        </div>
      </div>
    </header>
  );
};

export default Header;
