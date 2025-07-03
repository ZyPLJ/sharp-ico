import axios from '../axios'

export const uploadFileZip = (file, sizes = null) => {
    const formData = new FormData()
    formData.append('file', file)
    if (sizes) {
        formData.append('sizes', sizes)
    }
    
    return axios({
        method: 'post',
        url: '/uploadDownload/sizes',
        data: formData,
        headers: {
            'Content-Type': 'multipart/form-data'
        },
        responseType: 'blob'
    })
}

export const uploadFile = (file, sizes = null) => {
    const formData = new FormData()
    formData.append('file', file)
    if (sizes) {
        formData.append('sizes', sizes)
    }
    
    return axios({
        method: 'post',
        url: '/uploadDownload',
        data: formData,
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
}

export const dowloadFile = (fileName) => {
    return axios({
        method: 'get',
        url: `/downloads/${fileName}`,
        responseType: 'blob'
    })
}

export const getImageInfo = (fileName) => {
    return axios({
        method: 'get',
        url: `/getImageInfo/${fileName}`
    })
}