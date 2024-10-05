/* eslint-disable @typescript-eslint/no-explicit-any */
import Globe, { GlobeMethods } from "react-globe.gl";
import "./App.css";
import { useEffect, useRef, useState } from "react";
import * as THREE from "three";
import { createPortal } from "react-dom";
import Modal from "./components/Modal/Modal";

function App() {
  const [showPopup, setShowPopup] = useState(false);
  const globeEl = useRef<GlobeMethods | undefined>();
  const N = 30;
  const gData = [...Array(N).keys()].map(() => ({
    lat: (Math.random() - 0.5) * 180,
    lng: (Math.random() - 0.5) * 360,
    size: 10 + Math.random() * 30,
    color: ["red", "white", "blue", "green"][Math.round(Math.random() * 3)],
    title: "local",
  }));
  const markerSvg = `<svg viewBox="-4 0 36 36">
    <path fill="currentColor" d="M14,0 C21.732,0 28,5.641 28,12.6 C28,23.963 14,36 14,36 C14,36 0,24.064 0,12.6 C0,5.641 6.268,0 14,0 Z"></path>
    <circle fill="black" cx="14" cy="14" r="7"></circle>
  </svg>`;
  useEffect(() => {
    const globe = globeEl.current;

    // Auto-rotate

    if (globe) {
      // globe.controls().autoRotate = true;
      // globe.controls().autoRotateSpeed = 0.35;

      // Add clouds sphere
      const CLOUDS_IMG_URL = "./clouds.png"; // from https://github.com/turban/webgl-earth
      const CLOUDS_ALT = 0.004;
      const CLOUDS_ROTATION_SPEED = -0.006; // deg/frame
      new THREE.TextureLoader().load(CLOUDS_IMG_URL, (cloudsTexture) => {
        const clouds = new THREE.Mesh(
          new THREE.SphereGeometry(
            globe.getGlobeRadius() * (1 + CLOUDS_ALT),
            75,
            75
          ),
          new THREE.MeshPhongMaterial({ map: cloudsTexture, transparent: true })
        );
        globe.scene().add(clouds);

        (function rotateClouds() {
          clouds.rotation.y += (CLOUDS_ROTATION_SPEED * Math.PI) / 180;
          requestAnimationFrame(rotateClouds);
        })();
      });
    }
  }, []);

  return (
    <div style={{ position: "relative" }}>
      {showPopup &&
        createPortal(
          <Modal onClose={() => setShowPopup(false)} />,
          document.body
        )}

      <div style={{ zIndex: 4, position: "relative" }}>
        <Globe
          ref={globeEl}
          animateIn={false}
          globeImageUrl="//unpkg.com/three-globe/example/img/earth-blue-marble.jpg"
          bumpImageUrl="//unpkg.com/three-globe/example/img/earth-topology.png"
          htmlElementsData={gData}
          htmlElement={(d: any) => {
            const el = document.createElement("div");
            el.innerHTML = markerSvg;
            el.style.color = d.color;
            el.style.width = `${d.size}px`;

            el.style.pointerEvents = "auto";
            el.style.cursor = "pointer";
            el.onclick = () => {
              setShowPopup(true);
            };

            return el;
          }}
        />
      </div>
    </div>
  );
}

export default App;
