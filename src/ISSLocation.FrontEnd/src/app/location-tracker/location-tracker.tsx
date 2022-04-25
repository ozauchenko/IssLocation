import React from 'react'
import { useState } from 'react'
import './location-tracker.css'
import { IssLocation } from '../../api/types'
import { useEffect } from 'react'
import Map from '../google-map'
import LocationInfo from '../location-info'
import { LoadingSpinner } from '../loading-spinner/loading-spinner'

interface LocationTrackerState {
    loading: boolean;
    location: IssLocation;
}

const initialLocation = {
    position: {
        latitude: 0,
        longitude: 0
    },
    message: '',
    timestamp: 0
}

export const LocationTracker = () => {
    
    const [state, setState] = useState<LocationTrackerState>({
        loading: true,
        location: initialLocation
    });

    useEffect(() => {
        const getLocation = async () => {
            try {
                let response = await fetch('https://localhost:7077/api/issLocation/location');
            const data = await response.json();
                setState({
                    location: data,
                    loading: false
                });
            } catch (err) {
                setState( state => ({
                    ...state,
                    loading: true
                }));
                alert(err)
            }    
        }
        getLocation();
        const interval = setInterval(() => getLocation(), 5000);
        return () => {
            clearInterval(interval);
        }
    }, [])
        
    return (
        <>
             {state.loading ?
                 <LoadingSpinner/> :
                 <div className='container'>
                    <LocationInfo location={state.location}/>
                    <Map position={state.location.position}/>
                 </div> 
             }
        </>
    )
}