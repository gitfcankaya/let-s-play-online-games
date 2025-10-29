import React from 'react';
import './Footer.css';

const Footer: React.FC = () => {
  return (
    <footer className="footer">
      <div className="container">
        <div className="footer-content">
          <div className="footer-section">
            <h3>About</h3>
            <p>Play thousands of free online games. We add new games daily!</p>
          </div>
          
          <div className="footer-section">
            <h3>Categories</h3>
            <ul>
              <li><a href="/category/action">Action</a></li>
              <li><a href="/category/puzzle">Puzzle</a></li>
              <li><a href="/category/racing">Racing</a></li>
              <li><a href="/category/sports">Sports</a></li>
            </ul>
          </div>
          
          <div className="footer-section">
            <h3>Support</h3>
            <ul>
              <li><a href="/privacy">Privacy Policy</a></li>
              <li><a href="/terms">Terms of Service</a></li>
              <li><a href="/contact">Contact Us</a></li>
            </ul>
          </div>
          
          <div className="footer-section">
            <h3>Follow Us</h3>
            <div className="social-links">
              <a href="https://facebook.com" target="_blank" rel="noopener noreferrer">Facebook</a>
              <a href="https://twitter.com" target="_blank" rel="noopener noreferrer">Twitter</a>
              <a href="https://instagram.com" target="_blank" rel="noopener noreferrer">Instagram</a>
            </div>
          </div>
        </div>
        
        <div className="footer-bottom">
          <p>&copy; 2025 GamePlatform. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
