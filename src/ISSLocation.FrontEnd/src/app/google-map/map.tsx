import React from 'react'
import './google-map.css'
import issIcon from '../assets/issIcon.png'
import { GoogleMap, LoadScript, Marker } from '@react-google-maps/api'
import { Position } from '../../api/types'

interface Props {
    position: Position;
}

export const Map = React.memo((props: Props) => {

    const {latitude, longitude} = props.position;
    
    return (
        // @ts-ignore
            <LoadScript
                googleMapsApiKey={`${process.env.REACT_APP_GOOGLE_API_KEY}`}
            >
                <GoogleMap
                    mapContainerClassName='google-map-container'
                    center={ {
                        lat: 17.598847,
                        lng: 15.460203
                    }}
                    zoom={2}
                >
                    <Marker icon={issIcon}
                            position={{ lat: latitude, lng: longitude }}
                            draggable={false}
                    />
                </GoogleMap>
            </LoadScript>
    )
})