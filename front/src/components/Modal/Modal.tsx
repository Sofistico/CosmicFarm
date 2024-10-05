import CloseIcon from "../../assets/CloseIcon";
import styles from "./Modal.module.scss";

interface props {
  onClose: () => void;
}
const Modal = ({ onClose }: props) => {
  return (
    <div className={styles.overlay}>
      <div className={styles.modalContainer}>
        <div className={styles.closeContainer}>
          <button onClick={onClose}>
            <CloseIcon />
          </button>
        </div>
        Teste
      </div>
    </div>
  );
};

export default Modal;
