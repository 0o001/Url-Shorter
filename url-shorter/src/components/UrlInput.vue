<template>
  <div class="item">
    <div class="details">
      <form class="mt-10" onsubmit="return false" v-if="!shorted">   
        <label for="longUrl" ref="longUrl" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-gray-300">Long URL</label>
        <div class="relative">
          <input type="url" id="longUrl" class="block p-4 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Long URL Address" required>
          <button type="submit" id="longUrlBtn" class="text-white absolute right-2.5 bottom-2.5 bg-green-700 hover:bg-green-800 focus:ring-4 focus:outline-none focus:ring-green-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800" @click="getShortUrl">GO!</button>
        </div>
      </form>
      <form class="mt-10" onsubmit="return false" v-else>   
        <label for="shortUrl" ref="shortUrl" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-gray-300">Short URL</label>
        <div class="relative">
          <input type="text" id="shortUrl" class="block p-4 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" :value="$data.shortUrl" disabled>
          <button type="submit" id="copyBtn" class="text-white absolute right-2.5 bottom-2.5 bg-green-700 hover:bg-green-800 focus:ring-4 focus:outline-none focus:ring-green-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800" @click="getCopy">Copy!</button>
        </div>
        <div class="relative text-center mt-3">
          <a @click="refreshPage" class="text-center" href="#">Short Another URL</a>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>
.item {
  margin-top: 2rem;
  display: flex;
}

.details {
  flex: 1;
  margin-left: 1rem;
}

i {
  display: flex;
  place-items: center;
  place-content: center;
  width: 32px;
  height: 32px;

  color: var(--color-text);
}

h3 {
  font-size: 1.2rem;
  font-weight: 500;
  margin-bottom: 0.4rem;
  color: var(--color-heading);
}

@media (min-width: 1024px) {
  .item {
    margin-top: 0;
    padding: 0.4rem 0 1rem calc(var(--section-gap) / 2);
  }

  i {
    top: calc(50% - 25px);
    left: -26px;
    position: absolute;
    border: 1px solid var(--color-border);
    background: var(--color-background);
    border-radius: 8px;
    width: 50px;
    height: 50px;
  }

  .item:before {
    content: ' ';
    border-left: 1px solid var(--color-border);
    position: absolute;
    left: 0;
    bottom: calc(50% + 25px);
    height: calc(50% - 25px);
  }

  .item:after {
    content: ' ';
    border-left: 1px solid var(--color-border);
    position: absolute;
    left: 0;
    top: calc(50% + 25px);
    height: calc(50% - 25px);
  }

  .item:first-of-type:before {
    display: none;
  }

  .item:last-of-type:after {
    display: none;
  }
}
</style>

<script lang="ts">
import { getCurrentInstance } from 'vue'

export default {
  data() {
    return {
      shorted: false,
      API_URL: getCurrentInstance()?.appContext.config.globalProperties.API_URL,
      shortUrl: ''
    }
  },
  methods: {
    getShortUrl(): void {
      const longUrlInput = this.$el.querySelector('#longUrl')
      const longUrl = longUrlInput.value
      if(!this.isValidURL(longUrl)) {
        return;
      }

      fetch(`${this.API_URL}/ShortUrl/?longUrl=${longUrl}`, { method: 'POST' })
      .then((data) => data.json())
      .then((data) => {
        if(data.shortUrlCode) {
          this.shorted = true
          this.shortUrl = window.location.href + data.shortUrlCode
        } else {
          throw Error()
        }
      })
      .catch(() => {
        alert('Error! Something went wrong. Please try again.')
      })
    },
    isValidURL(url: string): boolean {
      try {
        return !!new URL(url)
      } catch {
        return false
      }
    },
    getCopy() {
      const shortUrlInput = this.$el.querySelector('#shortUrl')
      const copyBtn = this.$el.querySelector('#copyBtn')
      navigator.clipboard.writeText(shortUrlInput.value)
      copyBtn.innerHTML = 'Copied!'
    },
    refreshPage() {
      this.$router.go(0);
    }
  }
}
</script>
