import React, { useEffect, useState } from 'react';
import { gameService } from '../services/gameService';
import { categoryService } from '../services/categoryService';
import GameCard from '../components/game/GameCard';
import type { Game, Category } from '../types';
import './Home.css';

const Home: React.FC = () => {
  const [featuredGames, setFeaturedGames] = useState<Game[]>([]);
  const [recentGames, setRecentGames] = useState<Game[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [featured, recent, cats] = await Promise.all([
          gameService.getAllGames(undefined, true),
          gameService.getAllGames(),
          categoryService.getAllCategories(),
        ]);
        setFeaturedGames(featured.slice(0, 6));
        setRecentGames(recent.slice(0, 12));
        setCategories(cats);
      } catch (error) {
        console.error('Error fetching data:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return <div className="loading">Loading...</div>;
  }

  return (
    <div className="home">
      <section className="hero">
        <div className="container">
          <h1>Welcome to GamePlatform</h1>
          <p>Play thousands of free online games</p>
          <div className="search-bar">
            <input type="text" placeholder="Search games..." />
            <button>Search</button>
          </div>
        </div>
      </section>

      <section className="categories-section">
        <div className="container">
          <h2>Categories</h2>
          <div className="categories-grid">
            {categories.map((category) => (
              <a href={`/category/${category.slug}`} key={category.id} className="category-card">
                <h3>{category.name}</h3>
                <p>{category.gamesCount} games</p>
              </a>
            ))}
          </div>
        </div>
      </section>

      <section className="games-section">
        <div className="container">
          <h2>Featured Games</h2>
          <div className="games-grid">
            {featuredGames.map((game) => (
              <GameCard key={game.id} game={game} />
            ))}
          </div>
        </div>
      </section>

      <section className="games-section">
        <div className="container">
          <h2>Recent Games</h2>
          <div className="games-grid">
            {recentGames.map((game) => (
              <GameCard key={game.id} game={game} />
            ))}
          </div>
        </div>
      </section>

      <section className="ad-section">
        <div className="container">
          <div className="ad-placeholder">
            <p>Advertisement Space (Google AdSense / Yandex)</p>
          </div>
        </div>
      </section>
    </div>
  );
};

export default Home;
