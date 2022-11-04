<template>
  <div>
    <div>Redirecting...</div>
  </div>
</template>
<script lang="ts">
import { getCurrentInstance } from 'vue'

export default {
  data() {
    return {
      shorted: false,
      API_URL: getCurrentInstance()?.appContext.config.globalProperties.API_URL,
      shortUrlCode: this.$route.params.shortUrlCode
    }
  },
  beforeMount() {
    fetch(`${this.API_URL}/ShortUrl/${this.shortUrlCode}`)
    .then((data) => data.json())
    .then((data) => {
      if(data.longUrl) {
        window.location.href = data.longUrl
      } else {
        throw Error()
      }
    })
    .catch(() => {
      this.$router.push({ name: 'home' })
    })
  }
}
</script>
