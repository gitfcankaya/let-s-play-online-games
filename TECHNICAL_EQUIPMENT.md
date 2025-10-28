# Teknik Ekipmanlar / Technical Equipment

Bu doküman, "Let's Play Online Games" projesi için yazılım geliştirme sürecinde kullanılan tüm teknik ekipmanları ve araçları listeler.

This document lists all technical equipment and tools used in the software development process for the "Let's Play Online Games" project.

## 1. Versiyon Kontrol Sistemi / Version Control System

### Git
- **Amaç / Purpose**: Kod versiyon kontrolü, işbirliği ve değişiklik takibi
- **Kullanım / Usage**: Tüm proje dosyalarının versiyonlanması
- **Platform**: GitHub (https://github.com/gitfcankaya/let-s-play-online-games)

### GitHub
- **Amaç / Purpose**: Uzak depo yönetimi, kod inceleme, proje yönetimi
- **Özellikler / Features**:
  - Repository hosting
  - Pull requests
  - Issues tracking
  - GitHub Actions (CI/CD)
  - Project boards

## 2. Geliştirme Ortamı / Development Environment

### IDE'ler / Integrated Development Environments
- **Visual Studio Code**: Hafif, genişletilebilir kod editörü
- **WebStorm**: Web geliştirme için profesyonel IDE
- **IntelliJ IDEA**: Tam özellikli geliştirme ortamı

### Metin Editörleri / Text Editors
- **Sublime Text**: Hızlı ve verimli metin editörü
- **Vim/Neovim**: Terminal tabanlı editör

## 3. Programlama Dilleri ve Framework'ler / Programming Languages & Frameworks

### Frontend
- **HTML5**: Web sayfası yapısı
- **CSS3/SCSS**: Stil ve tasarım
- **JavaScript**: İstemci tarafı programlama
- **TypeScript**: Tip güvenli JavaScript
- **React.js**: UI geliştirme framework'ü
- **Vue.js**: Progressive web framework
- **Angular**: Tam özellikli frontend framework

### Backend
- **Node.js**: Sunucu tarafı JavaScript runtime
- **Express.js**: Web uygulama framework'ü
- **Python**: Genel amaçlı programlama dili
- **Flask/Django**: Python web framework'leri
- **Java**: Enterprise uygulamalar için
- **Spring Boot**: Java framework

### Game Development
- **Phaser.js**: HTML5 oyun geliştirme framework'ü
- **Three.js**: 3D grafik kütüphanesi
- **Socket.io**: Gerçek zamanlı çift yönlü iletişim
- **WebGL**: 3D grafik için web standardı

## 4. Paket Yöneticileri / Package Managers

### npm (Node Package Manager)
- **Amaç / Purpose**: JavaScript paket yönetimi
- **Kullanım / Usage**: Bağımlılık yönetimi ve kurulumu

### Yarn
- **Amaç / Purpose**: Alternatif paket yöneticisi
- **Avantajlar / Benefits**: Daha hızlı kurulum, deterministik

### pip
- **Amaç / Purpose**: Python paket yönetimi

## 5. Build Tools ve Task Runners / Build Tools & Task Runners

### Webpack
- **Amaç / Purpose**: Modül paketleyici
- **Kullanım / Usage**: JavaScript, CSS ve diğer varlıkların derlenmesi

### Vite
- **Amaç / Purpose**: Modern frontend build tool
- **Avantajlar / Benefits**: Hızlı geliştirme sunucusu

### Gulp
- **Amaç / Purpose**: Task automation
- **Kullanım / Usage**: Build süreçlerinin otomasyonu

### Parcel
- **Amaç / Purpose**: Zero-config web uygulama paketleyici

## 6. Test Araçları / Testing Tools

### Unit Testing
- **Jest**: JavaScript test framework'ü
- **Mocha**: Esnek test framework'ü
- **Chai**: Assertion kütüphanesi
- **Jasmine**: Behavior-driven test framework

### End-to-End Testing
- **Cypress**: Modern web uygulamaları için E2E test
- **Selenium**: Tarayıcı otomasyonu
- **Puppeteer**: Chrome DevTools Protocol kullanımı
- **Playwright**: Cross-browser otomasyon

### Code Coverage
- **Istanbul/NYC**: Kod kapsama analizi
- **Codecov**: Kod kapsama raporlama

## 7. Kod Kalitesi ve Analiz / Code Quality & Analysis

### Linters
- **ESLint**: JavaScript/TypeScript linting
- **Prettier**: Kod formatlama
- **StyleLint**: CSS/SCSS linting

### Code Analysis
- **SonarQube**: Kod kalitesi analizi
- **CodeClimate**: Otomatik kod inceleme
- **JSHint**: JavaScript kod kalitesi

## 8. CI/CD (Continuous Integration/Deployment)

### GitHub Actions
- **Amaç / Purpose**: Otomatik build, test ve deployment
- **Özellikler / Features**:
  - Automated testing
  - Code quality checks
  - Deployment automation

### Alternatifler / Alternatives
- **Jenkins**: Açık kaynak otomasyon sunucusu
- **Travis CI**: Bulut tabanlı CI/CD
- **CircleCI**: Modern CI/CD platformu
- **GitLab CI/CD**: GitLab entegre CI/CD

## 9. Konteynerizasyon ve Sanallaştırma / Containerization & Virtualization

### Docker
- **Amaç / Purpose**: Konteyner tabanlı uygulama dağıtımı
- **Kullanım / Usage**: Tutarlı geliştirme ve üretim ortamları

### Docker Compose
- **Amaç / Purpose**: Multi-container uygulamaları tanımlama
- **Kullanım / Usage**: Geliştirme ortamı kurulumu

### Kubernetes
- **Amaç / Purpose**: Container orchestration
- **Kullanım / Usage**: Production deployment ve scaling

## 10. Veritabanları / Databases

### İlişkisel / Relational
- **PostgreSQL**: Açık kaynak ilişkisel veritabanı
- **MySQL**: Popüler ilişkisel veritabanı
- **SQLite**: Hafif dosya tabanlı veritabanı

### NoSQL
- **MongoDB**: Döküman tabanlı veritabanı
- **Redis**: In-memory veri yapısı deposu
- **Firebase**: Real-time veritabanı (oyunlar için ideal)

## 11. API Geliştirme ve Test / API Development & Testing

### API Testing
- **Postman**: API geliştirme ve test platformu
- **Insomnia**: REST API client
- **cURL**: Komut satırı HTTP client

### API Documentation
- **Swagger/OpenAPI**: API dokümantasyonu
- **Postman Documentation**: Otomatik API dokümanı

## 12. Proje Yönetimi / Project Management

### Issue Tracking
- **GitHub Issues**: Entegre issue tracking
- **Jira**: Profesyonel proje yönetimi
- **Trello**: Görsel proje yönetimi

### Agile Tools
- **GitHub Projects**: Kanban board
- **Asana**: İş takibi ve yönetimi
- **Monday.com**: Ekip işbirliği platformu

## 13. İletişim Araçları / Communication Tools

### Takım İletişimi / Team Communication
- **Slack**: Takım mesajlaşma platformu
- **Microsoft Teams**: İşbirliği platformu
- **Discord**: Sesli ve yazılı iletişim

### Dokümantasyon / Documentation
- **Confluence**: Wiki ve dokümantasyon
- **Notion**: All-in-one workspace
- **GitBook**: Modern dokümantasyon

## 14. Tasarım Araçları / Design Tools

### UI/UX Design
- **Figma**: Collaborative design tool
- **Adobe XD**: UI/UX tasarım platformu
- **Sketch**: Digital design toolkit

### Grafik ve Asset'ler / Graphics & Assets
- **Adobe Photoshop**: Görsel düzenleme
- **GIMP**: Açık kaynak görsel düzenleme
- **Aseprite**: Pixel art ve animasyon (oyunlar için)

## 15. Monitoring ve Logging / Monitoring & Logging

### Application Monitoring
- **New Relic**: Uygulama performans izleme
- **Datadog**: Infrastructure monitoring
- **Sentry**: Error tracking ve monitoring

### Logging
- **ELK Stack**: (Elasticsearch, Logstash, Kibana)
- **Splunk**: Log analizi ve monitoring
- **Winston**: Node.js logging kütüphanesi

## 16. Cloud Platformları / Cloud Platforms

### Hosting ve Deployment
- **AWS (Amazon Web Services)**: Bulut altyapısı
  - EC2: Virtual servers
  - S3: Object storage
  - Lambda: Serverless computing
- **Google Cloud Platform**: Google'ın bulut servisleri
- **Microsoft Azure**: Microsoft bulut platformu
- **Heroku**: PaaS provider
- **Netlify**: Frontend hosting
- **Vercel**: Next.js ve frontend deployment

### Game-Specific Hosting
- **Photon**: Multiplayer oyun sunucusu
- **PlayFab**: Backend platform (Microsoft)
- **Amazon GameLift**: AWS oyun sunucusu servisi
- **Unity Gaming Services**: Unity entegre backend servisleri

## 17. Güvenlik Araçları / Security Tools

### Güvenlik Analizi / Security Analysis
- **Snyk**: Dependency vulnerability scanning
- **OWASP ZAP**: Web security testing
- **npm audit**: NPM paket güvenlik kontrolü

### Authentication & Authorization
- **OAuth 2.0**: Authorization framework
- **JWT**: JSON Web Tokens
- **Auth0**: Authentication platform
- **Firebase Authentication**: Google auth servisi

## 18. Performans Optimizasyonu / Performance Optimization

### Profiling ve Debugging
- **Chrome DevTools**: Tarayıcı geliştirici araçları
- **React DevTools**: React debugging
- **Redux DevTools**: State management debugging

### Performance Testing
- **Lighthouse**: Web performance audit
- **WebPageTest**: Website performance test
- **GTmetrix**: Performance monitoring

## 19. Dokümantasyon Araçları / Documentation Tools

### Code Documentation
- **JSDoc**: JavaScript dokümantasyon
- **TypeDoc**: TypeScript dokümantasyon
- **Doxygen**: Multi-language dokümantasyon

### README ve Markdown
- **Markdown**: Dokümantasyon formatı
- **GitHub Markdown**: GitHub flavored markdown

## 20. Diğer Yardımcı Araçlar / Other Utility Tools

### Version Management
- **nvm**: Node version manager
- **pyenv**: Python version management

### Terminal Tools
- **iTerm2**: macOS terminal emulator
- **Hyper**: Cross-platform Electron-based terminal
- **Windows Terminal**: Modern terminal for Windows
- **Oh My Zsh**: Zsh framework
- **tmux**: Terminal multiplexer

### Browser Extensions
- **React Developer Tools**: React component inspection
- **Vue.js devtools**: Vue component inspection
- **Redux DevTools**: Redux state management (also mentioned in performance section for debugging)
- **Wappalyzer**: Technology profiler

---

## Notlar / Notes

Bu liste, projenin ihtiyaçlarına göre güncellenebilir ve genişletilebilir. Her geliştirici kendi tercihlerine göre araç seçimi yapabilir, ancak ekip standartları ve best practice'lere uyulması önerilir.

This list can be updated and expanded based on project needs. Each developer can choose tools based on their preferences, but following team standards and best practices is recommended.

## Lisans ve Maliyet / License & Cost

Araçların çoğu açık kaynak veya ücretsiz katmanları mevcut. Ticari projeler için lisans gereksinimlerini kontrol ediniz.

Most tools are open source or have free tiers. Check license requirements for commercial projects.
