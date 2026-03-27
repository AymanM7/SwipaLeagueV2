const coreSteps = [
  'Drive',
  'Hit the ball',
  'Move it toward the goal',
  'Jump, boost, or dunk',
  'Score — then instant reset',
] as const

const features = [
  { title: 'Car movement', desc: 'Tight arcade steering built for readable ball contact.', status: 'mvp' as const },
  { title: 'Ball physics', desc: 'Tuned bounce and friction so hits feel fair, not random.', status: 'mvp' as const },
  { title: 'Goals + scoring', desc: 'Normal, aerial, and dunk goals with clear point tiers.', status: 'mvp' as const },
  { title: 'Jump + double jump', desc: 'Chain air time for jukes and aerial touches.', status: 'mvp' as const },
  { title: 'Boost', desc: 'Short burst of speed to close distance or chase the ball.', status: 'mvp' as const },
  { title: 'Dunk mechanic', desc: 'Reward aggressive plays when you slam it home near the hoop.', status: 'mvp' as const },
  { title: '1v1 & 2v2', desc: 'Squad up or duel — networking after the prototype feels right.', status: 'soon' as const },
  { title: 'Training mode', desc: 'Drills for boosts, wall reads, and dunk timing.', status: 'soon' as const },
] as const

const scoreTiers = [
  { label: 'Goal', points: 2, detail: 'Grounded — clean finish through the hoop.' },
  { label: 'Aerial', points: 3, detail: 'Ball last touched while you were in the air.' },
  { label: 'Dunk', points: 4, detail: 'Finish from the dunk zone with air or style momentum.' },
] as const

const novel = [
  {
    title: 'Style Chain',
    desc: 'Combo bonus for wall ride → aerial touch → dunk. Risky routes, bigger hype.',
  },
  {
    title: 'Dynamic Goals',
    desc: 'Hoop size and placement shift in overtime — adapt or lose the swing.',
  },
  {
    title: 'Arena Gadgets',
    desc: 'Bounce pads, gravity lanes, and rebound walls that reward map reads.',
  },
  {
    title: 'Momentum Meter',
    desc: 'Big plays fill a hype bar — short bonuses when the crowd (or you) goes loud.',
  },
] as const

function App() {
  return (
    <div className="min-h-svh bg-[#070b12] font-sans text-slate-300 antialiased">
      <div className="pointer-events-none fixed inset-0 opacity-[0.35] [background-image:radial-gradient(circle_at_20%_20%,rgba(34,211,238,0.14),transparent_45%),radial-gradient(circle_at_80%_0%,rgba(232,121,249,0.12),transparent_40%),radial-gradient(circle_at_50%_100%,rgba(251,191,36,0.08),transparent_45%)]" />

      <header className="relative border-b border-slate-800/80 bg-[#070b12]/80 backdrop-blur-md">
        <div className="mx-auto flex max-w-6xl items-center justify-between px-6 py-4">
          <a href="#" className="flex items-center gap-2 text-white">
            <span className="h-2 w-2 rounded-full bg-cyan-400 shadow-[0_0_12px_rgba(34,211,238,0.8)]" />
            <span className="text-sm font-semibold tracking-tight">R Swipe League V2</span>
          </a>
          <nav className="hidden items-center gap-8 text-sm font-medium text-slate-400 sm:flex">
            <a className="transition hover:text-white" href="#loop">
              Loop
            </a>
            <a className="transition hover:text-white" href="#features">
              Features
            </a>
            <a className="transition hover:text-white" href="#scoring">
              Scoring
            </a>
            <a className="transition hover:text-white" href="#roadmap">
              Roadmap
            </a>
          </nav>
          <a
            href="#play"
            className="rounded-full bg-gradient-to-r from-cyan-400 to-sky-500 px-4 py-2 text-xs font-semibold text-slate-950 shadow-[0_0_24px_rgba(34,211,238,0.35)] transition hover:brightness-110 sm:text-sm"
          >
            Play prototype
          </a>
        </div>
      </header>

      <main className="relative mx-auto max-w-6xl px-6 pb-24 pt-16 sm:pt-20">
        <section id="hero" className="grid gap-12 lg:grid-cols-[1.15fr_0.85fr] lg:items-center">
          <div>
            <p className="mb-4 inline-flex items-center gap-2 rounded-full border border-cyan-500/30 bg-cyan-500/10 px-3 py-1 text-xs font-semibold uppercase tracking-widest text-cyan-200">
              Arcade physics · car soccer meets hoops
            </p>
            <h1 className="text-4xl font-semibold tracking-tight text-white sm:text-5xl lg:text-6xl lg:leading-[1.05]">
              <span className="mb-3 block text-3xl font-bold tracking-tight text-white sm:text-4xl lg:text-5xl">
                R Swipe League V2
              </span>
              <span className="block text-3xl sm:text-4xl lg:text-5xl">
                Drive hard.{' '}
                <span className="bg-gradient-to-r from-cyan-300 via-sky-400 to-amber-300 bg-clip-text text-transparent">
                  Send it aerial.
                </span>{' '}
                Dunk the win.
              </span>
            </h1>
            <p className="mt-6 max-w-xl text-lg leading-relaxed text-slate-400">
              A fast, readable physics playground: hit the ball, set up jump chains and boost lines, then finish
              through the hoop. Miss or get blocked — zero points. Pure arcade clarity.
            </p>
            <div id="play" className="mt-8 flex flex-wrap gap-4">
              <a
                href="#features"
                className="rounded-full bg-white px-6 py-3 text-sm font-semibold text-slate-950 transition hover:bg-slate-200"
              >
                See what ships first
              </a>
              <a
                href="#loop"
                className="rounded-full border border-slate-700 px-6 py-3 text-sm font-semibold text-white transition hover:border-cyan-400/50 hover:text-cyan-200"
              >
                Core loop
              </a>
            </div>
            <dl className="mt-10 grid gap-6 sm:grid-cols-3">
              <div className="rounded-2xl border border-slate-800 bg-[#0f1624]/90 p-4">
                <dt className="text-xs font-semibold uppercase tracking-wide text-slate-500">Feel</dt>
                <dd className="mt-1 text-sm font-medium text-white">Easy to learn</dd>
                <dd className="text-xs text-slate-500">Tight resets, no downtime</dd>
              </div>
              <div className="rounded-2xl border border-slate-800 bg-[#0f1624]/90 p-4">
                <dt className="text-xs font-semibold uppercase tracking-wide text-slate-500">Challenge</dt>
                <dd className="mt-1 text-sm font-medium text-white">Hard to master</dd>
                <dd className="text-xs text-slate-500">Style chains & gadgets</dd>
              </div>
              <div className="rounded-2xl border border-slate-800 bg-[#0f1624]/90 p-4">
                <dt className="text-xs font-semibold uppercase tracking-wide text-slate-500">Now</dt>
                <dd className="mt-1 text-sm font-medium text-white">First playable</dd>
                <dd className="text-xs text-slate-500">One arena, one car, prove the fun</dd>
              </div>
            </dl>
          </div>

          <div className="relative rounded-3xl border border-slate-800 bg-gradient-to-b from-[#0f1624] to-[#070b12] p-6 shadow-[0_0_0_1px_rgba(148,163,184,0.05)_inset]">
            <div className="absolute -right-6 -top-6 hidden h-24 w-24 rounded-full bg-amber-400/20 blur-2xl lg:block" />
            <p className="text-xs font-semibold uppercase tracking-widest text-slate-500">Prototype focus</p>
            <h2 className="mt-2 text-2xl font-semibold text-white">Two-minute fun test</h2>
            <p className="mt-3 text-sm leading-relaxed text-slate-400">
              We are validating whether driving + jumping + dunking stays satisfying before networking and meta
              systems land. Physics stay controlled and readable — no mystery bounces.
            </p>
            <ul className="mt-6 space-y-3 text-sm text-slate-300">
              <li className="flex gap-3">
                <span className="mt-1 h-1.5 w-1.5 shrink-0 rounded-full bg-cyan-400" />
                Arena scale tuned for 10–20s crosses — instant replays, no hiking.
              </li>
              <li className="flex gap-3">
                <span className="mt-1 h-1.5 w-1.5 shrink-0 rounded-full bg-fuchsia-400" />
                Dunk zones grant clarity, not chaos — assists stay optional and honest.
              </li>
              <li className="flex gap-3">
                <span className="mt-1 h-1.5 w-1.5 shrink-0 rounded-full bg-amber-300" />
                Momentum meter and gadgets arrive once the core loop sings.
              </li>
            </ul>
            <div className="mt-8 rounded-2xl border border-dashed border-cyan-500/30 bg-cyan-500/5 p-4 text-xs leading-relaxed text-cyan-100/90">
              Open the Unity project in the repo (`unity/`), press Play, and try to chain a boost → aerial touch →
              dunk. If it feels electric, we scale up.
            </div>
          </div>
        </section>

        <section id="loop" className="mt-24">
          <div className="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between">
            <div>
              <p className="text-xs font-semibold uppercase tracking-widest text-cyan-300/80">Core loop</p>
              <h2 className="mt-2 text-3xl font-semibold text-white sm:text-4xl">Drive → hit → dunk → reset</h2>
            </div>
            <p className="max-w-md text-sm text-slate-400">
              Every possession is a micro-story: approach, read the bounce, commit to air or boost, cash or wipe.
            </p>
          </div>
          <ol className="mt-10 grid gap-4 md:grid-cols-5">
            {coreSteps.map((step, i) => (
              <li
                key={step}
                className="group relative overflow-hidden rounded-2xl border border-slate-800 bg-[#0f1624] p-4 transition hover:border-cyan-500/40"
              >
                <span className="text-xs font-semibold text-cyan-300/70">0{i + 1}</span>
                <p className="mt-2 text-sm font-semibold text-white">{step}</p>
                <div className="pointer-events-none absolute inset-x-0 bottom-0 h-1 bg-gradient-to-r from-cyan-500/0 via-cyan-400/60 to-cyan-500/0 opacity-0 transition group-hover:opacity-100" />
              </li>
            ))}
          </ol>
        </section>

        <section id="features" className="mt-24">
          <div className="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between">
            <div>
              <p className="text-xs font-semibold uppercase tracking-widest text-fuchsia-300/80">MVP + beyond</p>
              <h2 className="mt-2 text-3xl font-semibold text-white sm:text-4xl">Feature roster</h2>
            </div>
          </div>
          <div className="mt-10 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
            {features.map((f) => (
              <article
                key={f.title}
                className="flex flex-col rounded-2xl border border-slate-800 bg-[#0c1220] p-5 transition hover:border-slate-600"
              >
                <div className="flex items-center justify-between gap-2">
                  <h3 className="text-base font-semibold text-white">{f.title}</h3>
                  <span
                    className={
                      f.status === 'mvp'
                        ? 'rounded-full bg-emerald-500/15 px-2 py-0.5 text-[10px] font-semibold uppercase tracking-wide text-emerald-300'
                        : 'rounded-full bg-amber-500/15 px-2 py-0.5 text-[10px] font-semibold uppercase tracking-wide text-amber-200'
                    }
                  >
                    {f.status === 'mvp' ? 'Prototype' : 'Coming soon'}
                  </span>
                </div>
                <p className="mt-3 flex-1 text-sm leading-relaxed text-slate-400">{f.desc}</p>
              </article>
            ))}
          </div>
        </section>

        <section id="scoring" className="mt-24">
          <div className="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between">
            <div>
              <p className="text-xs font-semibold uppercase tracking-widest text-amber-200/80">Scoring</p>
              <h2 className="mt-2 text-3xl font-semibold text-white sm:text-4xl">Risk pays in points</h2>
            </div>
            <p className="max-w-md text-sm text-slate-400">Missed shots and blocks stay at zero — only clean hoops bank.</p>
          </div>
          <div className="mt-10 grid gap-4 lg:grid-cols-3">
            {scoreTiers.map((tier) => (
              <div
                key={tier.label}
                className="relative overflow-hidden rounded-2xl border border-slate-800 bg-gradient-to-b from-[#101a2c] to-[#0a101c] p-6"
              >
                <div className="flex items-baseline gap-2">
                  <span className="text-4xl font-semibold text-white">{tier.points}</span>
                  <span className="text-sm font-medium text-slate-400">pts</span>
                </div>
                <h3 className="mt-2 text-lg font-semibold text-amber-100">{tier.label}</h3>
                <p className="mt-2 text-sm leading-relaxed text-slate-400">{tier.detail}</p>
                <div className="pointer-events-none absolute -right-8 -top-8 h-24 w-24 rounded-full border border-white/5" />
              </div>
            ))}
          </div>
        </section>

        <section id="roadmap" className="mt-24">
          <div className="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between">
            <div>
              <p className="text-xs font-semibold uppercase tracking-widest text-slate-400">Novel systems</p>
              <h2 className="mt-2 text-3xl font-semibold text-white sm:text-4xl">What makes the meta spicy</h2>
            </div>
          </div>
          <div className="mt-10 grid gap-4 md:grid-cols-2">
            {novel.map((item) => (
              <div key={item.title} className="rounded-2xl border border-slate-800 bg-[#0f1624]/80 p-6">
                <h3 className="text-lg font-semibold text-white">{item.title}</h3>
                <p className="mt-3 text-sm leading-relaxed text-slate-400">{item.desc}</p>
              </div>
            ))}
          </div>
        </section>
      </main>

      <footer className="relative border-t border-slate-800/80 bg-[#080d14]">
        <div className="mx-auto flex max-w-6xl flex-col gap-6 px-6 py-10 sm:flex-row sm:items-center sm:justify-between">
          <div>
            <p className="text-sm font-semibold text-white">R Swipe League V2</p>
            <p className="mt-1 text-xs text-slate-500">Unity 6 prototype · React marketing shell</p>
          </div>
          <div className="flex flex-wrap gap-4 text-xs text-slate-400">
            <a className="hover:text-white" href="https://twitter.com" target="_blank" rel="noreferrer">
              X / Twitter
            </a>
            <a className="hover:text-white" href="https://discord.com" target="_blank" rel="noreferrer">
              Discord
            </a>
            <a className="hover:text-white" href="mailto:hello@example.com">
              Press kit
            </a>
          </div>
        </div>
      </footer>
    </div>
  )
}

export default App
