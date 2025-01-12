
using System.Numerics;
using System.Security.Cryptography;
using ZeroElectric.Vinculum;

namespace LunarLander
{
    internal class Lander
    {
        Image testi1;
        Image testi2; 
        static void Main(string[] args)
        {
            Lander game = new Lander();
            game.Init();
            game.GameLoop();
        }

        /////////////////////////////////////

        // Pelaajan sijainti
        // x on aluksen keskikohta
        // y on aluksen pohja
        float x = 120;
        float y = 16;

        // Onko moottori päällä
        bool engine_on = false;
        bool gameResult;

        // Pelaajan nopeus, polttoaine ja polttonopeus
        float velocity = 0;
        float fuel = 100;
        float fuel_consumption = 10.0f;

        // Laskeutumisalustan katon sijainti y-akselilla. Y kasvaa alaspäin.
        int landing_y = 125;

        // Ruudunpäivitykseen menevä aika (oletus)
        float delta_time = 1.0f / 60.0f;

        // Moottorin voimakkuus
        float acceleration = 20.9f;

        // Painovoiman voimakkuus
        float gravity = 9.89f;

        // Pelialueen ja ikkunan mittasuhteet
        int game_width = 240;
        int game_height = 136;
        // Rectangle GameRec = new Rectangle(0,0, game_width );
        int screen_width = 1280;
        int screen_height = 720;

        // TODO: Lisää tekstuuri johon peli piirretään skaalausta varten
        Texture skaalaustextuuri = new();

        void Init()
        {
            // TODO: Aloita Raylib ja luo ikkuna.
            Raylib.InitWindow(screen_width, screen_height, "W1");
            Raylib.SetTargetFPS(60);
            skaalaustextuuri = Raylib.LoadTexture("ship.png");
        }

        void GameLoop()
        {
            // TODO: Pyöritä gamelooppia niin kauan
            // kun Raylibin ikkuna on auki
            while(!Raylib.WindowShouldClose())
            {
                Update();
                Draw();
                // TODO: Kun peli on piirretty, skaalaa se isommaksi
                // Raylib.ImageDraw(testi1, testi2, Rectangle srcRec, Rectangle dstRec, Color tint);
            }
            Raylib.CloseWindow();
            // TODO: Sulje Raylib ikkuna
        }


        void Update()
        {
            // TODO: Kysy Raylibiltä miten pitkään yksi ruudunpäivitys kesti
            delta_time = Raylib.GetFrameTime(); // Kirjoita funktiokutsu 0.0f tilalle.

            // Lisää painovoiman vaikutus
            velocity += gravity * delta_time;

            // TODO: Kun pelaaja painaa nappia (esim nuoli ylös)
            // ja polttoainetta on jäljellä, lisää
            // kiihtyvyys nopeuteen
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && fuel > 0)
            {
                velocity -= acceleration * delta_time;
                fuel -= fuel_consumption * delta_time;
                engine_on = true;
            }
            else
            {
                engine_on = false;
            }
            // Liikuta alusta
            y += velocity * delta_time;

            if (y >= landing_y && gravity != 0)
            {

                if (velocity < 5)
                {
                    //win
                    gameResult = true;
                    ENDGame();
                }
                else
                {
                    //lose
                    gameResult = false;
                    ENDGame();
                }
            }
        }

        void ENDGame()
        {
            acceleration = 0;
            fuel = 0;
            gravity = 0;
            velocity = 0;
        }

        void Draw()
        {
            // TODO: Aloita piirtäminen
            Raylib.BeginDrawing();
            // TODO: Tyhjennä ruutu mustaksi
            Raylib.ClearBackground(Raylib.BLACK);

            int plat_x = (int)x - 30;
            int plat_y = landing_y;
            int plat_w = 60;
            int plat_h = 10;
            // TODO: Piirrä laskeutumisalusta suorakulmiona
            Raylib.DrawRectangle(plat_x, plat_y, plat_w, plat_h, Raylib.GRAY);
            // (plat_x, plat_y, plat_w, plat_h)

            // TODO: Piirrä aluksen kuva kolmion sijaan
            // EN!! :D
            Raylib.DrawTriangle(new Vector2(x, y - 30), new Vector2(x - 10, y), new Vector2(x + 10, y), Raylib.SKYBLUE);
            Raylib.DrawTexture(skaalaustextuuri, (int)x, (int)y, Raylib.WHITE);
            // Piirrä moottorin liekki
            if (engine_on)
            {
                Raylib.DrawTriangle(new Vector2(x - 5, y), new Vector2(x, y + 32), new Vector2(x + 5, y), Raylib.YELLOW);

            }

            // Piirrä polttoaineen tilanne
            Raylib.DrawRectangle(9, 9, 102, 12, Raylib.BLUE);
            Raylib.DrawRectangle(10, 10, (int)fuel, 10, Raylib.YELLOW);
            Raylib.DrawText("FUEL", 11, 11, 12, Raylib.DARKBLUE);

            // Piirrä debug tietoja
            Raylib.DrawText($"V:{velocity}", 11, 31, 8, Raylib.WHITE);
            Raylib.DrawText($"dt:{delta_time}", 11, 41, 8, Raylib.WHITE);

            // TODO: Lopeta piirtäminen
            Raylib.EndDrawing();
        }
    }
}
