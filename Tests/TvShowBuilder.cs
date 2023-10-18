using Bogus;
using Domain;

namespace Tests
{
	internal class TvShowBuilder
	{
        private readonly Faker<TvShow> _tvShowFaker = new Faker<TvShow>();

        internal TvShowBuilder WithExternalId(int externalId)
		{
			_tvShowFaker.RuleFor(tv => tv.ExternalId, externalId);
			return this;
		}

        internal TvShowBuilder WithPremiereDate(DateTime premiered)
        {
            _tvShowFaker.RuleFor(tv => tv.Premiered, premiered);
            return this;
        }

        public TvShow Build()
        {
            return _tvShowFaker.Generate();
        }
    }
}

