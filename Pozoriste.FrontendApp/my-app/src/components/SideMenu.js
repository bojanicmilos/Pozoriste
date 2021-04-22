import React from 'react'
import '../style/style.css'
import { Link } from 'react-router-dom'
import { getRole } from './globalStorage/RoleCheck'
import { isUserLogged } from './globalStorage/IsUserLogged'
import AddTheatre from './Admin/TheatreActions/AddTheatre'
import theatre from '../images/theatre.png'
import actor from '../images/actor.png'
import hall from '../images/hall.png'
import piece from '../images/piece.png'
import show from '../images/show.png'

const SideMenu = () => {

    return (
        <div className='side-menu'>
            <Link className='side-menu-element' to="/showlist"><img className="img-show" src={show} alt="hall" style={{ height: '40px', width: '40px' }} />Predstave</Link>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addshow" >Dodaj predstavu </Link>
            }
            <span class="divider"></span>
            {

                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/piecealllist"><img className="img-piece" src={piece} alt="hall" style={{ height: '40px', width: '40px' }} />Pozorisni komadi</Link>
            }

            <Link className='side-menu-element' to="/pieceactivelist">Aktivni pozorisni komadi</Link>

            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addPiece">Dodaj komad</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/actorlist"><img className="img-actor" src={actor} alt="hall" style={{ height: '40px', width: '40px' }} />Svi glumci</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addactor">Dodaj glumca</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/showalltheatres"><img className="img-theatre" src={theatre} alt="hall" style={{ height: '40px', width: '40px' }} />Pozorista</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addtheatre">Dodaj pozoriste</Link>
            }
            <span class="divider"></span>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/showallAuditoriums"><img className="img-hall" src={hall} alt="hall" style={{ height: '40px', width: '40px' }} /> Sale</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addAuditorium">Dodaj salu</Link>
            }
        </div>
    )
}

export default SideMenu
