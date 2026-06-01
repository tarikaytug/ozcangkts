'use client';
import dynamic from 'next/dynamic';


// Haritayı sunucuda değil, YALNIZCA tarayıcıda yüklenecek şekilde çağırıyoruz (SSR: false)
const TaktikHarita = dynamic(() => import('../src/components/TaktikHarita'), { 
  ssr: false,
  // Harita yüklenene kadar ekranda Matrix vari bir yazılım bekleme ekranı çıksın
  loading: () => (
    <div className="flex h-screen items-center justify-center bg-black">
      <div className="text-green-500 font-mono text-xl animate-pulse tracking-widest">
        UYDU BAĞLANTISI KURULUYOR...
      </div>
    </div>
  )
});

export default function Home() {
  return (
    <main className="w-full h-screen bg-black overflow-hidden relative">
      {/* Üstte ufak bir komuta merkezi HUD arayüzü */}
      <div className="absolute top-4 left-4 z-[999] bg-black/80 border border-green-500/30 p-4 rounded-lg pointer-events-none">
        <h1 className="text-green-500 font-mono font-bold tracking-widest">G.K.T.S KOMUTA MERKEZİ</h1>
        <p className="text-green-500/70 font-mono text-xs mt-1">GÜVENLİ BAĞLANTI (AES-256)</p>
      </div>

      <TaktikHarita />
    </main>
  );
}