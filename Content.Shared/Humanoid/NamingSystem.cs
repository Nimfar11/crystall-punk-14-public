using Content.Shared.Humanoid.Prototypes;
using Content.Shared.Dataset;
using Robust.Shared.Random;
using Robust.Shared.Prototypes;
using Robust.Shared.Enums;

namespace Content.Shared.Humanoid
{
    /// <summary>
    /// Figure out how to name a humanoid with these extensions.
    /// </summary>
    public sealed class NamingSystem : EntitySystem
    {
        [Dependency] private readonly IRobustRandom _random = default!;
        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

        public string GetName(string species, Gender? gender = null)
        {
            // if they have an old species or whatever just fall back to human I guess?
            // Some downstream is probably gonna have this eventually but then they can deal with fallbacks.
            if (!_prototypeManager.TryIndex(species, out SpeciesPrototype? speciesProto))
            {
                speciesProto = _prototypeManager.Index<SpeciesPrototype>("Human");
                Log.Warning($"Unable to find species {species} for name, falling back to Human");
            }

            switch (speciesProto.Naming)
            {
                case SpeciesNaming.First:
                    return Loc.GetString("namepreset-first",
                        ("first", GetFirstName(speciesProto, gender)));
                case SpeciesNaming.TheFirstofLast:
                    return Loc.GetString("namepreset-thefirstoflast",
                        ("first", GetFirstName(speciesProto, gender)), ("last", GetLastName(speciesProto)));
                case SpeciesNaming.FirstDashFirst:
                    return Loc.GetString("namepreset-firstdashfirst",
                        ("first1", GetFirstName(speciesProto, gender)), ("first2", GetFirstName(speciesProto, gender)));
                case SpeciesNaming.FirstLast:
                default:
                    return Loc.GetString("namepreset-firstlast",
                        ("first", GetFirstName(speciesProto, gender)), ("last", GetLastName(speciesProto)));
            }
        }

        public string GetFirstName(SpeciesPrototype speciesProto, Gender? gender = null)
        {
            switch (gender)
            {
                case Gender.Male:
                    return Loc.GetString(_random.Pick(_prototypeManager.Index<LocalizedDatasetPrototype>(speciesProto.MaleFirstNames).Values)); //CrystallEdge name localization
                case Gender.Female:
                    return Loc.GetString(_random.Pick(_prototypeManager.Index<LocalizedDatasetPrototype>(speciesProto.FemaleFirstNames).Values)); //CrystallEdge name localization
                default:
                    if (_random.Prob(0.5f))
                        return Loc.GetString(_random.Pick(_prototypeManager.Index<LocalizedDatasetPrototype>(speciesProto.MaleFirstNames).Values)); //CrystallEdge name localization
                    else
                        return Loc.GetString(_random.Pick(_prototypeManager.Index<LocalizedDatasetPrototype>(speciesProto.FemaleFirstNames).Values)); //CrystallEdge name localization
            }
        }

        public string GetLastName(SpeciesPrototype speciesProto)
        {
            return Loc.GetString(_random.Pick(_prototypeManager.Index<LocalizedDatasetPrototype>(speciesProto.LastNames).Values)); //CrystallEdge name localization
        }
    }
}
